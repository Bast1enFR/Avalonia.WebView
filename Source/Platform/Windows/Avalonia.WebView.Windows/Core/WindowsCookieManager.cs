using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.WebView.Windows.Core;
public class WindowsCookieManager : IPlatformCookieManager
{
    private readonly CoreWebView2CookieManager _manager;

    public WindowsCookieManager(CoreWebView2CookieManager manager)
    {
        _manager = manager;
    }

    public Task SetCookieAsync(Uri uri, string name, string value, DateTimeOffset? expires = null, bool httpOnly = false, bool secure = true)
    {
        var cookie = _manager.CreateCookie(name, value, uri.Host, "/");
        cookie.IsHttpOnly = httpOnly;
        cookie.IsSecure = secure;
        if (expires != null)
            cookie.Expires = expires.Value.UtcDateTime;

        _manager.AddOrUpdateCookie(cookie);
        return Task.CompletedTask;
    }

    public async Task<string?> GetCookieAsync(Uri uri, string name)
    {
        var cookies = await _manager.GetCookiesAsync(uri.ToString());
        return cookies.FirstOrDefault(c => c.Name == name)?.Value;
    }

    public Task DeleteCookieAsync(Uri uri, string name)
    {
        var cookies = _manager.GetCookiesAsync(uri.ToString()).Result;
        _manager.DeleteCookie(cookies.FirstOrDefault(c => c.Name == name));
        return Task.CompletedTask;
    }

    public Task ClearAllAsync()
    {
        _manager.DeleteAllCookies();
        return Task.CompletedTask;
    }
}
