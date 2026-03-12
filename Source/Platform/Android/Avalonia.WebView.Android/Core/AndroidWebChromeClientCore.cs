using Android.Util;
using Java.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.WebView.Android.Core;
public class AndroidWebChromeClientCore : WebChromeClient
{
    private readonly AndroidWebViewCore _core;
    public event EventHandler<string?>? NewWindowRequested;

    public AndroidWebChromeClientCore(AndroidWebViewCore core)
    {
        _core = core;
    }

    public override bool OnCreateWindow(AndroidWebView? view, bool isDialog, bool isUserGesture, Message? resultMsg)
    {
        if (view == null || resultMsg?.Obj == null)
            return false;

        // On intercepte la demande d'ouverture
        var newWebView = new AndroidWebView(view.Context!);

        newWebView.SetWebViewClient(new AndroidWebViewClientCore(_core, newWebView, url =>
        {
            // On déclenche l’événement NewWindowRequested avec l'url visée
            NewWindowRequested?.Invoke(this, url);
        }));

        var transport = (AndroidWebView.WebViewTransport)resultMsg.Obj;

        // Attacher la WebView AVANT tout LoadUrl
        transport.WebView = newWebView;
        resultMsg.SendToTarget();

        return true;
    }
}
