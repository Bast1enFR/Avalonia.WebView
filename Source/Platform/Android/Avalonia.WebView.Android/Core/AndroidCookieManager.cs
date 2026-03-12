using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.WebView.Android.Core;
public class AndroidCookieManager : IPlatformCookieManager
{
    public Task SetCookieAsync(Uri uri, string name, string value, DateTimeOffset? expires = null, bool httpOnly = false, bool secure = true)
    {
        if (CookieManager.Instance == null)
            throw new InvalidOperationException("CookieManager is not available.");

        var cookie = $"{name}={value}; Path=/;";

        if (secure) cookie += " Secure;";
        if (httpOnly) cookie += " HttpOnly;";
        if (expires != null) cookie += $" Expires={expires.Value.UtcDateTime:R};";
        
        CookieManager.Instance.SetCookie(uri.ToString(), cookie);
        CookieManager.Instance.Flush();

        return Task.CompletedTask;
    }

    public Task<string?> GetCookieAsync(Uri uri, string name)
    {
        if (CookieManager.Instance == null)
            throw new InvalidOperationException("CookieManager is not available.");

        var cookies = CookieManager.Instance.GetCookie(uri.ToString());
        if (cookies == null) return Task.FromResult<string?>(null);

        var parts = cookies.Split(';');
        foreach (var part in parts)
        {
            var kv = part.Split('=');
            if (kv.Length == 2 && kv[0].Trim() == name)
                return Task.FromResult<string?>(kv[1].Trim());
        }

        return Task.FromResult<string?>(null);
    }

    public Task DeleteCookieAsync(Uri uri, string name)
    {
        if (CookieManager.Instance == null)
            throw new InvalidOperationException("CookieManager is not available.");

        CookieManager.Instance.SetCookie(uri.ToString(), $"{name}=; Expires=Thu, 01 Jan 1970 00:00:00 GMT;");
        CookieManager.Instance.Flush();
        return Task.CompletedTask;
    }

    public Task ClearAllAsync()
    {
        if (CookieManager.Instance == null)
            throw new InvalidOperationException("CookieManager is not available.");

        CookieManager.Instance.RemoveAllCookies(null);
        CookieManager.Instance.Flush();
        return Task.CompletedTask;
    }
}