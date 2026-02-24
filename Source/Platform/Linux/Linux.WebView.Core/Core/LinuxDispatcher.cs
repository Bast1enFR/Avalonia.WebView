namespace Linux.WebView.Core;

internal class LinuxDispatcher : ILinuxDispatcher
{
    public LinuxDispatcher()
    {

    }

    bool _isRunning = false;

    // Store references to pending callbacks to prevent GC collection before GTK invokes them
    private static readonly System.Collections.Concurrent.ConcurrentDictionary<int, GSourceFunc> _pendingCallbacks = new();
    private static int _nextCallbackId;

    bool ILinuxDispatcher.Start()
    {
        _isRunning = true;
        return true;
    }

    bool ILinuxDispatcher.Stop()
    {
        _isRunning = false;
        return true;
    }

    private static GSourceFunc CreateTrackedCallback(Action<IntPtr> body)
    {
        var id = System.Threading.Interlocked.Increment(ref _nextCallbackId);
        GSourceFunc callback = null!;
        callback = (data) =>
        {
            try
            {
                body(data);
            }
            finally
            {
                _pendingCallbacks.TryRemove(id, out _);
            }
            return false;
        };
        _pendingCallbacks.TryAdd(id, callback);
        return callback;
    }

    Task<bool> ILinuxDispatcher.InvokeAsync(Action action)
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));
        
        if (!_isRunning)
            return Task.FromResult(false);

        var task = new TaskCompletionSource<bool>();
        var callback = CreateTrackedCallback((_) =>
        {
            try
            {
                action.Invoke();
                task.SetResult(true);
            }
            catch (Exception ex)
            {
                task.SetException(ex);
            }
        });
        Interop_glib.g_idle_add(callback, IntPtr.Zero);
        return task.Task;
    }
    
    Task<bool> ILinuxDispatcher.InvokeAsync(Action<object?, EventArgs> action)
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));
        
        if (!_isRunning)
            return Task.FromResult(false);

        var task = new TaskCompletionSource<bool>();
        var callback = CreateTrackedCallback((_) =>
        {
            try
            {
                action.Invoke(null, EventArgs.Empty);
                task.SetResult(true);
            }
            catch (Exception ex)
            {
                task.SetException(ex);
            }
        });
        Interop_glib.g_idle_add(callback, IntPtr.Zero);
        return task.Task;
    }
    
    Task<bool> ILinuxDispatcher.InvokeAsync(object? sender, EventArgs args, Action<object?, EventArgs> action)
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));
        
        if (!_isRunning)
            return Task.FromResult(false);

        var task = new TaskCompletionSource<bool>();
        var callback = CreateTrackedCallback((_) =>
        {
            try
            {
                action.Invoke(sender, args);
                task.SetResult(true);
            }
            catch (Exception ex)
            {
                task.SetException(ex);
            }
        });
        Interop_glib.g_idle_add(callback, IntPtr.Zero);
        return task.Task;
    }
    
    Task<T> ILinuxDispatcher.InvokeAsync<T>(Func<T> func)
    {
        if (func is null)
            throw new ArgumentNullException(nameof(func));
        
        if (!_isRunning)
            return Task.FromResult<T>(default(T)!);

        var task = new TaskCompletionSource<T>();
        var callback = CreateTrackedCallback((_) =>
        {
            try
            {
                var ret = func.Invoke();
                task.SetResult(ret);
            }
            catch (Exception ex)
            {
                task.SetException(ex);
            }
        });
        Interop_glib.g_idle_add(callback, IntPtr.Zero);
        return task.Task;
    }
    
    Task<T> ILinuxDispatcher.InvokeAsync<T>(Func<object?, EventArgs, T> func)
    {
        if (func is null)
            throw new ArgumentNullException(nameof(func));
        
        if (!_isRunning)
            return Task.FromResult<T>(default(T)!);

        var task = new TaskCompletionSource<T>();
        var callback = CreateTrackedCallback((_) =>
        {
            try
            {
                var ret = func.Invoke(null, EventArgs.Empty);
                task.SetResult(ret);
            }
            catch (Exception ex)
            {
                task.SetException(ex);
            }
        });
        Interop_glib.g_idle_add(callback, IntPtr.Zero);
        return task.Task;
    }
    
    Task<T> ILinuxDispatcher.InvokeAsync<T>(object? sender, EventArgs args, Func<object?, EventArgs, T> func)
    {
        if (func is null)
            throw new ArgumentNullException(nameof(func));
        
        if (!_isRunning)
            return Task.FromResult<T>(default(T)!);

        var task = new TaskCompletionSource<T>();
        var callback = CreateTrackedCallback((_) =>
        {
            try
            {
                var ret = func.Invoke(sender, args);
                task.SetResult(ret);
            }
            catch (Exception ex)
            {
                task.SetException(ex);
            }
        });
        Interop_glib.g_idle_add(callback, IntPtr.Zero);
        return task.Task;
    }
}
