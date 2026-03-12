namespace Avalonia.WebView.Android.Core;

partial class AndroidWebViewCore
{
    void RegisterEvents()
    {
        _handler.SizeChanged += HostControl_SizeChanged;
        _handler.PlatformHandlerChanged += Handler_PlatformHandlerChanged;
    }

    void UnregisterEvents()
    {
        _handler.SizeChanged -= HostControl_SizeChanged;
        _handler.PlatformHandlerChanged -= Handler_PlatformHandlerChanged;
    }

    void RegisterWebViewEvents(WebViewClient? wbClt)
    {
        if (wbClt is null)
            return;

        if (wbClt is AndroidWebViewClientCore androidWebViewClientCore)
        {
            androidWebViewClientCore.NavigationCompleted += WebViewClient_NavigationCompleted;
            androidWebViewClientCore.WebMessageReceived += WebViewClient_WebMessageReceived;
        }
        else
            return;
    }

    void RegisterWebChromeClient(WebChromeClient? webChromeClient)
    {
        if (webChromeClient is null)
            return;

        if (webChromeClient is AndroidWebChromeClientCore androidWebChromeClientCore)
        {
            androidWebChromeClientCore.NewWindowRequested += WebChromeClient_NewWindowRequested;
            return;
        }
    }

    private void HostControl_SizeChanged(object? sender, SizeChangedEventArgs e)
    {
        //e.Handled = true;
    }

    private void Handler_PlatformHandlerChanged(object? sender, EventArgs e)
    {

    }
}

