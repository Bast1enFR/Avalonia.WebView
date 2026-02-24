using Linux.WebView.Core;

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
}
