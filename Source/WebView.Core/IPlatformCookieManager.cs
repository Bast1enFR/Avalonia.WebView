using System;
using System.Collections.Generic;
using System.Text;

namespace WebViewCore;
public interface IPlatformCookieManager
{
    Task SetCookieAsync(Uri uri, string name, string value, DateTimeOffset? expires = null, bool httpOnly = false, bool secure = true);
    Task<string?> GetCookieAsync(Uri uri, string name);
    Task DeleteCookieAsync(Uri uri, string name);
    Task ClearAllAsync();
}
