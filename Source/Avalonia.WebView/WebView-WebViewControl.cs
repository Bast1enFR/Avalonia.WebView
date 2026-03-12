namespace AvaloniaWebView;

partial class WebView
{
    public bool CanGoForward => _platformWebView?.CanGoForward ?? false;

    public bool CanGoBack => _platformWebView?.CanGoBack ?? false;

    bool IWebViewControl.Navigate(Uri? uri)
    {
        if (uri is null)
            return false;

        if (_platformWebView is null || !_platformWebView.IsInitialized)
            return false;

        return _platformWebView.Navigate(uri);
    }

    bool IWebViewControl.NavigateToString(string htmlContent)
    {
        if (string.IsNullOrWhiteSpace(htmlContent))
            return false;

        if (_platformWebView is null || !_platformWebView.IsInitialized)
            return false;

        return _platformWebView.NavigateToString(htmlContent);
    }

    public bool GoBack()
    {
        if (_platformWebView is null || !_platformWebView.IsInitialized)
            return false;

        return _platformWebView.GoBack();
    }

    public bool GoForward()
    {
        if (_platformWebView is null || !_platformWebView.IsInitialized)
            return false;

        return _platformWebView.GoForward();
    }

    public bool Stop()
    {
        if (_platformWebView is null || !_platformWebView.IsInitialized)
            return false;

        return _platformWebView.Stop();
    }

    public bool Reload()
    {
        if (_platformWebView is null || !_platformWebView.IsInitialized)
            return false;

        return _platformWebView.Reload();
    }

    public Task<string?> ExecuteScriptAsync(string javaScript)
    {
        if (_platformWebView is null || !_platformWebView.IsInitialized)
            return Task.FromResult<string?>(default);

        return _platformWebView.ExecuteScriptAsync(javaScript);
    }

    public bool PostWebMessageAsJson(string webMessageAsJson, Uri? baseUri)
    {
        if (_platformWebView is null || !_platformWebView.IsInitialized)
            return false;

        return _platformWebView.PostWebMessageAsString(webMessageAsJson, baseUri);
    }

    public bool PostWebMessageAsString(string webMessageAsString, Uri? baseUri)
    {
        if (_platformWebView is null || !_platformWebView.IsInitialized)
            return false;

        return _platformWebView.PostWebMessageAsString(webMessageAsString, baseUri);
    }

    public bool OpenDevToolsWindow()
    {
        if (_platformWebView is null || !_platformWebView.IsInitialized)
            return false;

        return _platformWebView.OpenDevToolsWindow();
    }
    public IPlatformCookieManager? CookieManager
    {
        get
        {
            if (_platformWebView is null || !_platformWebView.IsInitialized)
                return null;
            return _platformWebView.CookieManager;
        }
    }
    public void ClearCache(bool reload = true)
    {
        if (_platformWebView is null || !_platformWebView.IsInitialized)
            return;
        _platformWebView.ClearCache(reload);
    }
    public void SetBasicAuthenticationCredentials(string username, string password)
    {
        if (_platformWebView is null || !_platformWebView.IsInitialized)
            return;
        _platformWebView.SetBasicAuthenticationCredentials(username, password);
    }
    public Task<MemoryStream> CaptureAsync()
    {
        if (_platformWebView is null || !_platformWebView.IsInitialized)
            return Task.FromResult(new MemoryStream());
        return _platformWebView.CaptureAsync();
    }
}
