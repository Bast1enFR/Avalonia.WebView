using Linux.WebView.Core;

namespace Avalonia.WebView.Linux.Core;

unsafe partial class LinuxWebViewCore
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

    private void HostControl_SizeChanged(object? sender, SizeChangedEventArgs e)
    {

    }

    private void Handler_PlatformHandlerChanged(object? sender, EventArgs e)
    {

    }

    void RegisterWebViewEvents(nint webView)
    {
        if (webView == IntPtr.Zero)
            return;

        var bRet = _dispatcher.InvokeAsync(() =>
        {
            GtkApi.AddSignalConnect(webView, "decide-policy", LinuxApplicationManager.LoadFunction(_decidePolicyArgsChanged), IntPtr.Zero);
            GtkApi.AddSignalConnect(webView, "permission-request", LinuxApplicationManager.LoadFunction(_permissionRequestHandler), IntPtr.Zero);
        }).Result;
    }

    void UnregisterWebViewEvents(nint webView)
    {
        if (webView == IntPtr.Zero)
            return;
    }


}
