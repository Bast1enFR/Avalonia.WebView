using Avalonia.Media.Imaging;
using Linux.WebView.Core;
using System.Net;

namespace Avalonia.WebView.Linux.Core;

public partial class LinuxWebViewCore : IPlatformWebView<LinuxWebViewCore>
{
    public bool IsInitialized
    {
        get => Volatile.Read(ref _isInitialized);
        private set => Volatile.Write(ref _isInitialized, value);
    }

    bool _isdisposed = false;
    public bool IsDisposed
    {
        get => Volatile.Read(ref _isdisposed);
        private set => Volatile.Write(ref _isdisposed, value);
    }

    public nint WebView
    {
        get
        {
            return _webView;
        }
    }

    public double ZoomFactor
    {
        get
        {
            return GtkApi.WebViewGetZoomLevel(_webView);
        }
        set
        {
            GtkApi.WebViewSetZoomLevel(_webView, value);
        }
    }

    delegate void void_nint_nint_nint(nint arg0, nint arg1, nint arg2);
    delegate bool bool_nint_nint_policytype(nint arg0, nint arg1, WebKitPolicyDecisionType type);
    delegate bool bool_nint_nint_nint(nint arg0, nint arg1, nint arg2);

    readonly nint _hostWindow;
    readonly nint _webView;
    readonly IntPtr _hostWindowX11Handle;
    readonly ILinuxApplication _application;
    readonly ILinuxDispatcher _dispatcher;
    readonly string _messageKeyWord;
    readonly IVirtualBlazorWebViewProvider? _provider;
    readonly IVirtualWebViewControlCallBack _callBack;
    readonly ViewHandler _handler;
    readonly WebViewCreationProperties _creationProperties;
    readonly string _dispatchMessageCallback = "__dispatchMessageCallback";

    readonly void_nint_nint_nint _userContentMessageReceived;
    readonly bool_nint_nint_policytype _decidePolicyArgsChanged;
    readonly bool_nint_nint_nint _permissionRequestHandler;

    WebScheme? _webScheme;
    bool _isInitialized = false;

    public LinuxWebViewCore(ILinuxApplication linuxApplication, ViewHandler handler, IVirtualWebViewControlCallBack callback, IVirtualBlazorWebViewProvider? provider, WebViewCreationProperties webViewCreationProperties)
    {
        _application = linuxApplication;
        _provider = provider;
        _messageKeyWord = "webview";
        _callBack = callback;
        _handler = handler;
        _creationProperties = webViewCreationProperties;

        _callBack.PlatformWebViewCreating(this, new WebViewCreatingEventArgs());

        _dispatcher = linuxApplication.Dispatcher;
        var gtkWrapper = linuxApplication.CreateWebView().Result;

        _hostWindow = gtkWrapper.window;
        _webView = gtkWrapper.webView;
        NativeHandler = gtkWrapper.hostHandle;
        _hostWindowX11Handle = gtkWrapper.hostHandle;

        _userContentMessageReceived = WebView_WebMessageReceived;
        _decidePolicyArgsChanged = WebView_DecidePolicy;
        _permissionRequestHandler = WebView_PermissionRequest;
        RegisterEvents();
    }

    ~LinuxWebViewCore()
    {
        Dispose(disposing: false);
    }
    private IPlatformCookieManager? _cookieManager;
    public IPlatformCookieManager CookieManager => _cookieManager!;
    private readonly NetworkCredential _basicAuthCred = new();
    public NetworkCredential BasicAuthenticationCredential => _basicAuthCred;
    public void ClearCache(bool reload = true)
    {
        nint ctx = GtkApi.WebViewGetContext(_webView);
        nint usrContentMgr = GtkApi.WebViewGetUserContentManager(_webView);

        if (_webView == IntPtr.Zero) return;

        var context = GtkApi.WebViewGetContext(_webView);
        if (context == IntPtr.Zero) return;

        var manager = GtkApi.WebViewGetWebsiteDataManager(_webView);
        if (manager == IntPtr.Zero) return;

        // Supprime toutes les données (cache disque, mémoire, etc.) depuis toujours (timespan = 0)
        GtkApi.WebViewWebsiteDataManagerClear(manager, WebKitWebsiteDataTypes.All, 0, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
    }
    public void SetBasicAuthenticationCredentials(string username, string password)
    {
        _basicAuthCred.UserName = username;
        _basicAuthCred.Password = password;
    }
    public async Task<MemoryStream> CaptureAsync()
    {
        if (_webView == 0)
            return new MemoryStream();

        var tcs = new TaskCompletionSource<MemoryStream>();

        // Callback natif  ==> wrapper en delegate managé
        GtkApi.GAsyncReadyCallback callback = (source, result, userData) =>
        {
            try
            {
                // Récupère le pixbuf
                nint error;
                nint pixbuf = GtkApi.WebViewGetSnapshotFinish(source, result, out error);

                if (pixbuf == 0)
                {
                    tcs.SetException(new Exception("Snapshot failed."));
                    return;
                }

                // Encode PNG dans un buffer natif
                if (!GtkApi.GetPixBuf(pixbuf, out nint bufferPtr, out nuint bufferSize, "png", 0, 0))
                {
                    tcs.SetException(new Exception("PNG encoding failed."));
                    return;
                }

                // Copie dans un MemoryStream
                var ms = new MemoryStream((int)bufferSize);
                byte[] managed = new byte[(int)bufferSize];
                Marshal.Copy(bufferPtr, managed, 0, (int)bufferSize);
                ms.Write(managed, 0, managed.Length);
                ms.Position = 0;

                tcs.SetResult(ms);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        };

        // Convertir le callback en pointeur natif
        var callbackPtr = Marshal.GetFunctionPointerForDelegate(callback);

        // Appel natif
        GtkApi.WebViewGetSnapshot(_webView, (uint)WebKitSnapshotRegion.FullDocument, (uint)WebKitSnapshotOptions.None, 0, callbackPtr, 0);

        return await tcs.Task;
    }
}
