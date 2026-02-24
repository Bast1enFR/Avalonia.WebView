namespace Linux.WebView.Core;

public interface ILinuxApplication : IDisposable
{
    bool IsRunning { get; }
    ILinuxDispatcher Dispatcher { get; }
    Task<bool> RunAsync(string? applicationName, string[]? args);
    Task<(nint window, nint webView, IntPtr hostHandle)> CreateWebView();
    Task StopAsync();
}
