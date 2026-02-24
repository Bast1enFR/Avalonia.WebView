using Linux.WebView.Core;

namespace Avalonia.WebView.Linux.Core;

unsafe partial class LinuxWebViewCore
{
    Task PrepareBlazorWebViewStarting(IVirtualBlazorWebViewProvider? provider, nint webView)
    {
        if (provider is null || WebView == IntPtr.Zero)
            return Task.CompletedTask;

        if (!provider.ResourceRequestedFilterProvider(this, out var filter))
            return Task.CompletedTask;

        _webScheme = filter;
        var bRet = _dispatcher.InvokeAsync(() =>
        {
            var context = GtkApi.WebViewGetContext(webView);
            _uriSchemeCallback = WebView_WebResourceRequest;
            GtkApi.WebContextRegisterUriScheme(context, filter.Scheme, LinuxApplicationManager.LoadFunction(_uriSchemeCallback), IntPtr.Zero);

            var userContentManager = GtkApi.WebViewGetUserContentManager(webView);

            var script = GtkApi.CreateUserScriptX(BlazorScriptHelper.BlazorStartingScript);
            GtkApi.AddScriptForUserContentManager(userContentManager, script);
            GtkApi.ReleaseScript(script);

            GtkApi.AddSignalConnect(userContentManager, $"script-message-received::{_messageKeyWord}", LinuxApplicationManager.LoadFunction(_userContentMessageReceived), IntPtr.Zero);
            GtkApi.RegisterScriptMessageHandler(userContentManager, _messageKeyWord);

        }).Result;

        return Task.CompletedTask;
    }

    uri_scheme_request_callback_delegate? _uriSchemeCallback;

    void ClearBlazorWebViewCompleted(nint webView)
    {
        if (webView == IntPtr.Zero)
            return;

        var bRet = _dispatcher.InvokeAsync(() =>
        {
        }).Result;
    }

    void WebView_WebMessageReceived(nint pContentManager, nint pJsResult, nint pArg)
    {
        if (_provider is null)
            return;

        var pJsStringValue = GtkApi.GetJavaScriptValue(pJsResult);
        if (!pJsStringValue.IsStringEx())
            return;

        var message = new WebViewMessageReceivedEventArgs
        {
            Message = pJsStringValue.ToStringEx(),
            Source = _provider.BaseUri,
        };
        GtkApi.ReleaseJavaScriptResult(pJsResult);

        _callBack.PlatformWebViewMessageReceived(this, message);
        _provider?.PlatformWebViewMessageReceived(this, message);
    }

    unsafe void WebView_WebResourceRequest(nint requestHandle, nint userData)
    {
        if (_provider is null)
            return;

        if (_webScheme is null)
            return;

        var scheme = GtkApi.UriSchemeRequestGetScheme(requestHandle);
        if (scheme != _webScheme.Scheme)
            return;

        var requestUri = GtkApi.UriSchemeRequestGetUri(requestHandle);
        var allowFallbackOnHostPage = _webScheme.BaseUri.IsBaseOfPage(requestUri);
        var requestWrapper = new WebResourceRequest
        {
            RequestUri = requestUri,
            AllowFallbackOnHostPage = allowFallbackOnHostPage,
        };

        var bRet = _provider.PlatformWebViewResourceRequested(this, requestWrapper, out var response);
        if (!bRet)
            return;

        if (response is null)
            return;

        var headerString = response.Headers[QueryStringHelper.ContentTypeKey];
        using var ms = new MemoryStream();
        response.Content.CopyTo(ms);

        var pBuffer = GtkApi.MarshalToGLibInputStream(ms.GetBuffer(), ms.Length);
        GtkApi.UriSchemeRequestFinish(requestHandle, pBuffer, ms.Length, headerString);
    }

}
