using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Webkit;
using Java.Interop;

namespace Avalonia.WebView.Android.Handlers;
public class JsBridge : Java.Lang.Object
{
    public event EventHandler<string>? MessageReceived;

    [JavascriptInterface]
    [Export("postMessage")]
    public void PostMessage(string message)
    {
        // Rebasculer sur le thread UI Android
        var handler = new Handler(Looper.MainLooper!);
        handler.Post(() =>
        {
            MessageReceived?.Invoke(this, message);
        });
    }
}

