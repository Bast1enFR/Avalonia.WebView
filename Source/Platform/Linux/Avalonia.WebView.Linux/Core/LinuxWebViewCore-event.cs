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
        if (_hostWindow == IntPtr.Zero)
            return;

        double scale = 1;
        var topLevel = TopLevel.GetTopLevel(_handler);
        if (topLevel is not null)
            scale = topLevel.RenderScaling;

        int width = Math.Max(1, Convert.ToInt32(e.NewSize.Width * scale));
        int height = Math.Max(1, Convert.ToInt32(e.NewSize.Height * scale));

        _ = _dispatcher.InvokeAsync(() =>
        {
            Interop_gtk.gtk_window_resize(_hostWindow, width, height);
        });
    }

    private void Handler_PlatformHandlerChanged(object? sender, EventArgs e)
    {
        if (_hostWindow == IntPtr.Zero)
            return;

        double scale = 1;
        var topLevel = TopLevel.GetTopLevel(_handler);
        if (topLevel is not null)
            scale = topLevel.RenderScaling;

        int width = Math.Max(1, Convert.ToInt32(_handler.Bounds.Width * scale));
        int height = Math.Max(1, Convert.ToInt32(_handler.Bounds.Height * scale));

        _ = _dispatcher.InvokeAsync(() =>
        {
            Interop_gtk.gtk_window_resize(_hostWindow, width, height);
            Interop_gtk.gtk_widget_show_all(_hostWindow);
        });
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
