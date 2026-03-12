using WebViewCore.Enums;

namespace Avalonia.WebView.Android.Core;

partial class AndroidWebViewCore
{
    Task<bool> PrepareBlazorWebViewStarting(AndroidWebView webView, IVirtualBlazorWebViewProvider? provider)
    {
        if (webView is null)
            return Task.FromResult(false);

        if (provider is null)
            return Task.FromResult(false);

        if (!provider.ResourceRequestedFilterProvider(this, out var filter))
            return Task.FromResult(false);

        _webViewClient = new AvaloniaWebViewClient(this, _callBack, provider, filter);
        _webChromeClient = new AvaloniaWebChromeClient(this);
        webView.SetWebViewClient(_webViewClient);
        webView.SetWebChromeClient(_webChromeClient);
        _isBlazorWebView = true;
        return Task.FromResult(true);
    }

    void ClearBlazorWebViewCompleted()
    {
        _isBlazorWebView = false;
    }
    private void WebViewClient_NavigationCompleted(object? sender, string? e)
    {
        _callBack.PlatformWebViewNavigationCompleted(this, new WebViewUrlLoadedEventArg() { IsSuccess = true, RawArgs = e });
    }
    private void WebViewClient_WebMessageReceived(object? sender, string? e)
    {
        var message = new WebViewMessageReceivedEventArgs
        {
            Message = e ?? "",
            MessageAsJson = "",
            Source = new Uri(_webView.Url ?? ""),
            RawArgs = e,
        };
        _callBack.PlatformWebViewMessageReceived(this, message);
        _provider?.PlatformWebViewMessageReceived(this, message);
    }
    private void WebChromeClient_NewWindowRequested(object? sender, string? url)
    {
        if (url is null)
            return;

        var urlLoadingStrategy = UrlRequestStrategy.OpenInWebView;
        var uri = new Uri(url!);

        if (_provider is not null)
        {
            if (_provider.BaseUri.IsBaseOf(uri) == true)
                urlLoadingStrategy = UrlRequestStrategy.OpenInWebView;
        }

        var newWindowEventArgs = new WebViewNewWindowEventArgs()
        {
            Url = uri,
            UrlLoadingStrategy = urlLoadingStrategy,
            RawArgs = url,
        };

        if (!_callBack.PlatformWebViewNewWindowRequest(this, newWindowEventArgs))
            return;

        switch (newWindowEventArgs.UrlLoadingStrategy)
        {
            case UrlRequestStrategy.OpenExternally:
                OpenUriHelper.OpenInProcess(uri);
                break;
            case UrlRequestStrategy.OpenInWebView:
                //e.NewWindow = CoreWebView2!;
                /*if (view?.Context is not null)
                {
                    var requestUrl = view.GetHitTestResult().Extra;
                    var intent = new Intent(Intent.ActionView, AndroidUri.Parse(requestUrl));
                    intent.SetFlags(ActivityFlags.NewTask);
                    view.Context.StartActivity(intent);
                }*/
                break;
            case UrlRequestStrategy.CancelLoad:
                break;
            case UrlRequestStrategy.OpenInNewWindow:
            default:
                break;
        }
    }
}

