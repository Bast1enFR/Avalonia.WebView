namespace Linux.WebView.Core;

internal class LinuxApplication : ILinuxApplication
{
    static LinuxApplication()
    {
    }

    public LinuxApplication(bool isWslDevelop)
    {
        _isWslDevelop = isWslDevelop;
        _dispatcher = new LinuxDispatcher();
    }

    ~LinuxApplication()
    {
        Dispose(disposing: false);
    }

    private readonly bool _isWslDevelop;
    readonly ILinuxDispatcher _dispatcher;
    Thread? _appThread;
    nint _defaultDisplay;

    bool _isRunning = false;
    public bool IsRunning
    {
        get => _isRunning;
        protected set => _isRunning = value;
    }

    bool _isDisposed;
    public bool IsDisposed
    {
        get => _isDisposed;
        protected set => _isDisposed = value;
    }

    bool ILinuxApplication.IsRunning => IsRunning;

    ILinuxDispatcher ILinuxApplication.Dispatcher => _dispatcher;

    Task<bool> ILinuxApplication.RunAsync(string? applicationName, string[]? args)
    {
        if (IsRunning)
            return Task.FromResult(true);

        var tcs = new TaskCompletionSource<bool>();

        _appThread = new Thread(()=> Run(tcs))
        {
            Name = "GTK3WORKINGTHREAD",
            IsBackground = true,
        };
        _appThread.Start();

        return tcs.Task;
    }

    void Run(TaskCompletionSource<bool> taskSource)
    {
        if (!_isWslDevelop)
                Interop_gdk.gdk_set_allowed_backends("x11");
        Environment.SetEnvironmentVariable("WAYLAND_DISPLAY", "/proc/fake-display-to-prevent-wayland-initialization-by-gtk3");

        try
        {
            if (!Interop_gtk.gtk_init_check(0, IntPtr.Zero))
            {
                taskSource.SetResult(false);
                return;
            }
            _dispatcher.Start();

            _defaultDisplay = Interop_gdk.gdk_display_get_default();
            IsRunning = true;
            taskSource.SetResult(true);
            Interop_gtk.gtk_main();
        }
        catch
        {
            taskSource.SetResult(false);
        }
    }

    Task ILinuxApplication.StopAsync()
    {
        if (!IsRunning)
            return Task.CompletedTask;

        _dispatcher.Stop();
        Interop_gtk.gtk_main_quit();
        _appThread?.Join();
        return Task.CompletedTask;
    }

    protected virtual async void Dispose(bool disposing)
    {
        if (!IsDisposed)
        {
            if (disposing)
            {
            }

            await ((ILinuxApplication)this).StopAsync();

            _defaultDisplay = IntPtr.Zero;

            IsDisposed = true;
        }
    }

    void IDisposable.Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    Task<(nint window, nint webView, IntPtr hostHandle)> ILinuxApplication.CreateWebView()
    {
        if (!_isRunning) throw new InvalidOperationException(nameof(IsRunning));
        return _dispatcher.InvokeAsync(() =>
        {
            nint window = Interop_gtk.gtk_window_new(GtkWindowType.GTK_WINDOW_TOPLEVEL);
            Interop_gtk.gtk_window_set_title(window, "WebView.Gtk.Window");
            Interop_gtk.gtk_window_set_keep_above(window, true);
            nint webView = GtkApi.WebViewNew();
            var settings = GtkApi.WebViewGetSettings(webView);
            GtkApi.SettingsSetEnableFullscreen(settings, true);
            Interop_gtk.gtk_container_add(window, webView);
            Interop_gtk.gtk_widget_show_all(window);
            GtkApi.WidgetRealize(window);
            return (window, webView, GtkApi.GetWidgetXid(window));
        });
    }


}
