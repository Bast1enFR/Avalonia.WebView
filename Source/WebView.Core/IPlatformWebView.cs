namespace WebViewCore;

public interface IPlatformWebView : IWebViewControl, IDisposable, IAsyncDisposable
{
    bool IsInitialized { get; }
    object? PlatformViewContext { get; }
    IntPtr NativeHandler { get;} 
    Task<bool> Initialize();
    IPlatformCookieManager CookieManager { get; }
    void SetBasicAuthenticationCredentials(string username, string password);
    void ClearCache(bool reload = true);
}
