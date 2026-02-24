namespace Linux.WebView.Core;

internal class LinuxDispatcher : ILinuxDispatcher
{
    public LinuxDispatcher()
    {

    }

    bool _isRunning = false;
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

    private static void InvokeOnGtkThread(Action action, TaskCompletionSource<bool> task)
    {
        GSourceFunc callback = (_) =>
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
            return false; // Return false to remove the idle source
        };
        // prevent delegate from being collected
        GC.KeepAlive(callback);
        Interop_glib.g_idle_add(callback, IntPtr.Zero);
    }

    Task<bool> ILinuxDispatcher.InvokeAsync(Action action)
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));
        
        if (!_isRunning)
            return Task.FromResult(false);

        var task = new TaskCompletionSource<bool>();
        InvokeOnGtkThread(action, task);
        return task.Task;
    }
    
    Task<bool> ILinuxDispatcher.InvokeAsync(Action<object?, EventArgs> action)
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));
        
        if (!_isRunning)
            return Task.FromResult(false);

        var task = new TaskCompletionSource<bool>();
        InvokeOnGtkThread(() => action.Invoke(null, EventArgs.Empty), task);
        return task.Task;
    }
    
    Task<bool> ILinuxDispatcher.InvokeAsync(object? sender, EventArgs args, Action<object?, EventArgs> action)
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));
        
        if (!_isRunning)
            return Task.FromResult(false);

        var task = new TaskCompletionSource<bool>();
        InvokeOnGtkThread(() => action.Invoke(sender, args), task);
        return task.Task;
    }
    
    Task<T> ILinuxDispatcher.InvokeAsync<T>(Func<T> func)
    {
        if (func is null)
            throw new ArgumentNullException(nameof(func));
        
        if (!_isRunning)
            return Task.FromResult<T>(default(T)!);

        var task = new TaskCompletionSource<T>();
        GSourceFunc callback = (_) =>
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
            return false;
        };
        GC.KeepAlive(callback);
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
        GSourceFunc callback = (_) =>
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
            return false;
        };
        GC.KeepAlive(callback);
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
        GSourceFunc callback = (_) =>
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
            return false;
        };
        GC.KeepAlive(callback);
        Interop_glib.g_idle_add(callback, IntPtr.Zero);
        return task.Task;
    }
}
