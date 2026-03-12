using Linux.WebView.Core;

namespace Avalonia.WebView.Linux.Core;

unsafe partial class LinuxWebViewCore
{
    void RegisterEvents()
    {
        _handler.SizeChanged += HostControl_SizeChanged;
        _handler.PlatformHandlerChanged += Handler_PlatformHandlerChanged;

        // AJOUTEZ CES LIGNES
        Console.WriteLine($"[WebView] RegisterEvents called - WebView handle: {_webView}");

        // Ajouter surveillance du chargement
        var loadChangedPtr = Marshal.GetFunctionPointerForDelegate<LoadChangedCallback>(OnLoadChanged);
        Interop_gtk.g_signal_connect(_webView, "load-changed", loadChangedPtr, IntPtr.Zero);

        Console.WriteLine("[WebView] Events registered");
    }

    // Ajoutez ces méthodes
    private void OnLoadChanged(nint webView, int loadEvent, nint userData)
    {
        Console.WriteLine($"[WebView] Load event: {loadEvent}");
    }

    // Ajoutez ce delegate
    delegate void LoadChangedCallback(nint webView, int loadEvent, nint userData);

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
