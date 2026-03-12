using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.WebView.Linux.Core;
public class LinuxCookieManager : IPlatformCookieManager
{
    public Task SetCookieAsync(Uri uri, string name, string value, DateTimeOffset? expires = null, bool httpOnly = false, bool secure = true)
    {
        return Task.CompletedTask;
    }
    public Task<string?> GetCookieAsync(Uri uri, string name)
    {
        return Task.FromResult<string?>(null);
    }
    public Task DeleteCookieAsync(Uri uri, string name)
    { 
        return Task.CompletedTask;
    }
    public Task ClearAllAsync()
    {
        return Task.CompletedTask;
    }
}
