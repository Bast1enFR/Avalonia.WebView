using System.Net;

namespace Avalonia.WebView.Android.Core;

public partial class AndroidWebViewCore : IPlatformWebView<AndroidWebViewCore>
{
    public AndroidWebViewCore(ViewHandler handler, IVirtualWebViewControlCallBack callback, IVirtualBlazorWebViewProvider? provider, WebViewCreationProperties webViewCreationProperties)
    {
        _provider = provider;
        _callBack = callback;
        _handler = handler;
        _creationProperties = webViewCreationProperties;

        _callBack.PlatformWebViewCreating(this, new WebViewCreatingEventArgs());
        AndroidWebView.SetWebContentsDebuggingEnabled(webViewCreationProperties.AreDevToolEnabled);

        var parentContext = AndroidApplication.Context;
        var webView = new AndroidWebView(parentContext)
        {
#pragma warning disable CS0618, CA1422  // Type or member is obsolete // Validate platform compatibility
            LayoutParameters = new AbsoluteLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent, 0, 0),
#pragma warning restore CS0618, CA1422 // Type or member is obsolete
        };
        webView.SetBackgroundColor(new AndroidColor(webViewCreationProperties.DefaultWebViewBackgroundColor.R, webViewCreationProperties.DefaultWebViewBackgroundColor.G, webViewCreationProperties.DefaultWebViewBackgroundColor.B));

        _webView = webView;
        NativeHandler = webView.Handle;
        RegisterEvents();
    }

    ~AndroidWebViewCore()
    {
        Dispose(disposing: false);
    }

    AndroidWebView _webView;
    readonly IVirtualBlazorWebViewProvider? _provider;
    readonly IVirtualWebViewControlCallBack _callBack;
    readonly ViewHandler _handler;
    readonly WebViewCreationProperties _creationProperties;

    bool _isBlazorWebView = false;

    bool _isInitialized = false;
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

    private double _zoomFactor = 1;
    public double ZoomFactor
    {
        get
        {
            if (_webView == null)
            {
                return _zoomFactor;
            }

#pragma warning disable CS0618 // Le type ou le membre est obsolète
            return _webView.Scale;
#pragma warning restore CS0618 // Le type ou le membre est obsolète
        }
        set
        {
            _zoomFactor = value;
            if (_webView != null)
            {
                _webView.SetInitialScale((int)(value * 100));
            }
        }
    }

    WebViewClient? _webViewClient;
    WebChromeClient? _webChromeClient;

    public AndroidWebView WebView
    {
        get => _webView;
        set => _webView = value;
    }

    public bool CanGoForward => throw new NotImplementedException();

    public bool CanGoBack => throw new NotImplementedException();

    private IPlatformCookieManager? _cookieManager;
    public IPlatformCookieManager CookieManager => _cookieManager!;
    private readonly NetworkCredential _basicAuthCred = new();
    public NetworkCredential BasicAuthenticationCredential => _basicAuthCred;    
    public void SetBasicAuthenticationCredentials(string username, string password)
    {
        _basicAuthCred.UserName =  username;
        _basicAuthCred.Password = password;
    }
    public void ClearCache(bool reload = true)
    {
        _webView.ClearCache(true);
        _webView.ClearHistory();

        var db = WebViewDatabase.GetInstance(_webView.Context);
        db?.ClearHttpAuthUsernamePassword();

        if (reload)
        {
            _webView.Reload();
        }
    }
}

