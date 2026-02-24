namespace Avalonia.WebView.Linux.Handlers;
internal class JavaScriptValueCallback
{
    public JavaScriptValueCallback(Action<string> callback)
    {
        _callback = callback;
    }

    readonly Action<string> _callback;
}
