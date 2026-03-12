using Android.Graphics;
using Canvas = Android.Graphics.Canvas;

namespace Avalonia.WebView.Android.Core;

partial class AndroidWebViewCore
{
    AndroidWebViewCore IPlatformWebView<AndroidWebViewCore>.PlatformView => this;

    public nint NativeHandler { get; private set; }

    bool IPlatformWebView.IsInitialized => IsInitialized;

    object? IPlatformWebView.PlatformViewContext => this;

    Task<string?> IWebViewControl.ExecuteScriptAsync(string javaScript)
    {        
        var webView = WebView;
        if (webView is null)
            return Task.FromResult<string?>(null);

        if (string.IsNullOrEmpty(javaScript))
            return Task.FromResult<string?>(null);

        var tcs = new TaskCompletionSource<string?>();

        try
        {
            webView.EvaluateJavascript(javaScript, new JavaScriptValueCallback(result =>
            {
                // Android renvoie souvent des chaînes JSON (ex: "\"hello\"")
                tcs.TrySetResult(result?.ToString());
            }));
        }
        catch (Exception ex)
        {
            tcs.TrySetException(ex);
        }

        return tcs.Task;
    }

    bool IWebViewControl.GoBack()
    {
        var webView = WebView;
        if (webView is null)
            return false;

        if (!webView.CanGoBack())
            return false;

        webView.GoBack();
        return true;
    }

    bool IWebViewControl.GoForward()
    {
        var webView = WebView;
        if (webView is null)
            return false;

        if (!webView.CanGoForward())
            return false;

        webView.GoForward();
        return true;
    }

    async Task<bool> IPlatformWebView.Initialize()
    {
        if (IsInitialized)
            return true;

        var webView = WebView;

        webView.Settings.SetSupportMultipleWindows(true);
        webView.Settings.JavaScriptEnabled = true;
        webView.Settings.DomStorageEnabled = true;
        webView.Settings.SetSupportZoom(true);
        webView.Settings.MixedContentMode = MixedContentHandling.AlwaysAllow;
        webView.Settings.AllowFileAccess = true;
        webView.Settings.AllowUniversalAccessFromFileURLs = true;
        webView.Settings.AllowFileAccessFromFileURLs = true;
        //webview.ZoomBy(1.2f);

        var bRet = await PrepareBlazorWebViewStarting(webView, _provider);
        if (!bRet)
        {
            _webViewClient = new AndroidWebViewClientCore(this);
            _webChromeClient = new AndroidWebChromeClientCore(this);
            webView.SetWebViewClient(_webViewClient);
            webView.SetWebChromeClient(_webChromeClient);

            var bridge = new JsBridge();
            bridge.MessageReceived += (_, msg) =>
            {
                Console.WriteLine("Message JS reçu : " + msg);
                // déclenches équivalent de WebMessageReceived
                ((AndroidWebViewClientCore)_webViewClient).OnWebMessageReceived(msg);
            };
            webView.AddJavascriptInterface(bridge, "nativeBridge");

            RegisterWebViewEvents(_webViewClient);
            RegisterWebChromeClient(_webChromeClient);
        }

        _cookieManager = new AndroidCookieManager();

        IsInitialized = true;
        _callBack.PlatformWebViewCreated(this, new WebViewCreatedEventArgs { IsSucceed = true });

        return true;
    }

    bool IWebViewControl.Navigate(Uri? uri)
    {
        if (uri is null)
            return false;

        var webView = WebView;
        if (webView is null)
            return false;

        webView.LoadUrl(uri.AbsoluteUri);
        return true;
    }

    bool IWebViewControl.NavigateToString(string htmlContent)
    {
        if (string.IsNullOrWhiteSpace(htmlContent))
            return false;

        var webView = WebView;
        if (webView is null)
            return false;

        webView.LoadData(htmlContent, default, default);
        return true;
    }

    bool IWebViewControl.OpenDevToolsWindow()
    {
        // Voir si possible d'ouvrir les devtools chrome sur pc de debug ??
        throw new NotImplementedException();
    }

    bool IWebViewControl.PostWebMessageAsJson(string webMessageAsJson, Uri? baseUri)
    {
        var webView = WebView;
        if (webView is null)
            return false;

        if (string.IsNullOrWhiteSpace(webMessageAsJson))
            return false;

        try
        {
            var basUri = _provider?.BaseUri;
            var androidUri = AndroidUri.Parse(baseUri?.AbsoluteUri);
            if (androidUri is null)
                return false;

            webView.PostWebMessage(new WebMessage(webMessageAsJson), androidUri);
            return true;
        }
        catch (Exception)
        {

        }

        return false;
    }

    bool IWebViewControl.PostWebMessageAsString(string webMessageAsString, Uri? baseUri)
    {
        var webView = WebView;
        if (webView is null)
            return false;

        if (string.IsNullOrWhiteSpace(webMessageAsString))
            return false;

        try
        {
            var basUri = _provider?.BaseUri;
            var androidUri = AndroidUri.Parse(baseUri?.AbsoluteUri);
            if (androidUri is null)
                return false;

            webView.PostWebMessage(new WebMessage(webMessageAsString), androidUri);
            return true;
        }
        catch (Exception)
        {
             
        }    

        return false;
    }

    bool IWebViewControl.Reload()
    {
        var webView = WebView;
        if (webView is null)
            return false;

        webView.Reload();
        return true;
    }

    bool IWebViewControl.Stop()
    {
        var webView = WebView;
        if (webView is null)
            return false;

        webView.StopLoading();
        return true;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!IsDisposed)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            ClearBlazorWebViewCompleted();
            UnregisterEvents();
            WebView.Dispose();
            WebView = default!;
            IsDisposed = true;
        }
    }

    void IDisposable.Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }


    ValueTask IAsyncDisposable.DisposeAsync()
    {
        ((IDisposable)this)?.Dispose();
        return new ValueTask();
    }
    public Task<MemoryStream> CaptureAsync()
    {
        var native = WebView; // à adapter selon ton wrapper
        var width = native.Width;
        var height = native.Height;

        if (width <= 0 || height <= 0)
            throw new InvalidOperationException("WebView not laid out yet.");
                
        var bitmap = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888!);

        var canvas = new Canvas(bitmap);
        native.Draw(canvas);

        var stream = new MemoryStream();
        bitmap.Compress(Bitmap.CompressFormat.Png!, 100, stream);
        stream.Position = 0;

        return Task.FromResult(stream);
    }

}

