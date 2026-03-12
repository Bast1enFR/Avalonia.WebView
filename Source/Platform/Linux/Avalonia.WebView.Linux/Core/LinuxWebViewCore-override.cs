namespace Avalonia.WebView.Linux.Core;

partial class LinuxWebViewCore
{
    public IntPtr NativeHandler { get; private set; }
    LinuxWebViewCore IPlatformWebView<LinuxWebViewCore>.PlatformView => this;

    bool IPlatformWebView.IsInitialized => IsInitialized;

    object? IPlatformWebView.PlatformViewContext => this;

    bool IWebViewControl.CanGoForward =>  _dispatcher.InvokeAsync(() => GtkApi.WebViewCanGoForward(WebView)).Result;

    bool IWebViewControl.CanGoBack => _dispatcher.InvokeAsync(() => GtkApi.WebViewCanGoBack(WebView)).Result;

    async Task<bool> IPlatformWebView.Initialize()
    {
        if (IsInitialized)
            return true;

        try
        {
            var bRet = _dispatcher.InvokeAsync(() =>
            {
                var settings = GtkApi.WebViewGetSettings(WebView);
                GtkApi.SettingsSetEnableDeveloperExtras(settings, _creationProperties.AreDevToolEnabled);
                GtkApi.SettingsSetAllowFileAccessFromFileUrls(settings, true);
                GtkApi.SettingsSetAllowModalDialogs(settings, true);
                GtkApi.SettingsSetAllowTopNavigationToDataUrls(settings, true);
                GtkApi.SettingsSetAllowUniversalAccessFromFileUrls(settings, true);
                GtkApi.SettingsSetEnableBackForwardNavigationGestures(settings, true);
                GtkApi.SettingsSetEnableCaretBrowsing(settings, false);
                GtkApi.SettingsSetEnableMediaCapabilities(settings, true);
                GtkApi.SettingsSetEnableMediaStream(settings, true);
                GtkApi.SettingsSetJavascriptCanAccessClipboard(settings, true);
                GtkApi.SettingsSetJavascriptCanOpenWindowsAutomatically(settings, true);
            }).Result;
            
            RegisterWebViewEvents(WebView);

            await PrepareBlazorWebViewStarting(_provider, WebView);

            _cookieManager = new LinuxCookieManager();

            IsInitialized = true;
            _callBack.PlatformWebViewCreated(this, new WebViewCreatedEventArgs { IsSucceed = true });

            return true;
        }
        catch (Exception ex)
        {
            _callBack.PlatformWebViewCreated(this, new WebViewCreatedEventArgs { IsSucceed = false, Message = ex.ToString() });
        }

        return false;
    }

    Task<string?> IWebViewControl.ExecuteScriptAsync(string javaScript)
    {
        if (string.IsNullOrWhiteSpace(javaScript))
            return Task.FromResult<string?>(default);

        var messageJSStringLiteral = HttpUtility.JavaScriptStringEncode(javaScript);
        var script = $"{_dispatchMessageCallback}((\"{messageJSStringLiteral}\"))";

        var bRet = _dispatcher.InvokeAsync(() =>
        {
            GtkApi.WebViewRunJavascript(WebView, script);
        }) .Result;

        return Task.FromResult<string?>(string.Empty);
    }

    bool IWebViewControl.GoBack()
    {
        return _dispatcher.InvokeAsync(() =>
        {
            if (!GtkApi.WebViewCanGoBack(WebView))
                return false;

            GtkApi.WebViewGoBack(WebView);
            return true;
        }).Result;
    }

    bool IWebViewControl.GoForward()
    {
        return _dispatcher.InvokeAsync(() =>
        {
            if (!GtkApi.WebViewCanGoForward(WebView))
                return false;

            GtkApi.WebViewGoForward(WebView);
            return true;
        }).Result;

    }

    bool IWebViewControl.Navigate(Uri? uri)
    {
        if (uri is null)
            return false;

        return _dispatcher.InvokeAsync(() => { GtkApi.WebViewLoadUri(WebView, uri.AbsoluteUri); return true; }).Result;
    }

    bool IWebViewControl.NavigateToString(string htmlContent)
    {
        if (string.IsNullOrWhiteSpace(htmlContent))
            return false;

       return  _dispatcher.InvokeAsync(() => { GtkApi.WebViewLoadHtml(WebView, htmlContent); return true; }).Result;
    }

    bool IWebViewControl.OpenDevToolsWindow()
    {
        return false;
    }

    bool IWebViewControl.PostWebMessageAsJson(string webMessageAsJson, Uri? baseUri)
    {
        if (string.IsNullOrWhiteSpace(webMessageAsJson))
            return false;

        var messageJSStringLiteral = HttpUtility.JavaScriptStringEncode(webMessageAsJson);
        var script = $"{_dispatchMessageCallback}((\"{messageJSStringLiteral}\"))";

       return _dispatcher.InvokeAsync(() =>
        {
            GtkApi.WebViewRunJavascript(WebView, script);
        }).Result;
 
    }

    bool IWebViewControl.PostWebMessageAsString(string webMessageAsString, Uri? baseUri)
    {
        if (string.IsNullOrWhiteSpace(webMessageAsString))
            return false;

        var messageJSStringLiteral = HttpUtility.JavaScriptStringEncode(webMessageAsString);
        var script = $"{_dispatchMessageCallback}((\"{messageJSStringLiteral}\"))";

       return _dispatcher.InvokeAsync(() =>
        {
            GtkApi.WebViewRunJavascript(WebView, script);
        }).Result; 
    }

    bool IWebViewControl.Reload() =>  _dispatcher.InvokeAsync(() => { GtkApi.WebViewReload(WebView); return true; }).Result;
    bool IWebViewControl.Stop() => _dispatcher.InvokeAsync(() => { GtkApi.WebViewStopLoading(WebView); return true; }).Result;

    protected virtual void Dispose(bool disposing)
    {
        if (!IsDisposed)
        {
            if (disposing)
            {
                try
                {
                    ClearBlazorWebViewCompleted(WebView);
                    UnregisterWebViewEvents(WebView);
                    UnregisterEvents();

                    var ret = _dispatcher.InvokeAsync(() =>
                    {
                        Interop_gtk.gtk_widget_destroy(_webView);
                        Interop_gtk.gtk_widget_destroy(_hostWindow);
                    }).Result;

                }
                catch (Exception)
                {

                }
            }

            IsDisposed = true;
        }
    }

    void IDisposable.Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    ValueTask IAsyncDisposable.DisposeAsync()
    {
        ((IDisposable)this)?.Dispose();
        return new ValueTask();
    }
}
