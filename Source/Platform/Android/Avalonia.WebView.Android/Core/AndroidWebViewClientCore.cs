using Android.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.WebView.Android.Core;
public class AndroidWebViewClientCore : WebViewClient
{
    private readonly AndroidWebViewCore _core;
    private readonly Action<string>? _onUrl;
    private AndroidWebView? _tempWebView;

    public event EventHandler<string?>? NavigationCompleted;
    public event EventHandler<string?>? WebMessageReceived;

    public AndroidWebViewClientCore(AndroidWebViewCore core)
    {
        _core = core;
    }
    public AndroidWebViewClientCore(AndroidWebViewCore core, AndroidWebView tempWebView, Action<string> onUrl)
    {
        _core = core;
        _tempWebView = tempWebView;
        _onUrl = onUrl;
    }
    public override bool ShouldOverrideUrlLoading(AndroidWebView? view, IWebResourceRequest? request)
    {
        var url = request?.Url?.ToString();
        if (url == null || view == null)
            return false;

        // Callback Avalonia
        _onUrl?.Invoke(url);    

        if (_tempWebView != null)
        {
            // Détruire la WebView temporaire
            var temp = _tempWebView;
            _tempWebView = null;

            temp!.Post(() =>
            {
                temp.StopLoading();
                temp.Destroy();
            });
        }

        // On bloque la popup native
        return false;
    }
    public override void OnPageFinished(AndroidWebView? view, string? url)
    {
        base.OnPageFinished(view, url);
        view?.EvaluateJavascript(@"
            window.chrome = window.chrome || {};
            window.chrome.webview = {
                postMessage: function(msg) {
                    nativeBridge.postMessage(msg);
                }
            };
        ", null);

        NavigationCompleted?.Invoke(this, url);
    }
    public void OnWebMessageReceived(string? message)
    {
        WebMessageReceived?.Invoke(this, message);
    }
    public override void OnReceivedHttpAuthRequest(AndroidWebView? view, HttpAuthHandler? handler, string? host, string? realm)
    {
        if (handler == null)
            return;
        Log.Debug("WEBVIEW", $"[AUTH REQUEST] Host: {host}, Realm: {realm}");
        
        if (!string.IsNullOrEmpty(_core.BasicAuthenticationCredential.UserName) && (!string.IsNullOrEmpty(_core.BasicAuthenticationCredential.Password)))
        {
            Log.Debug("WEBVIEW", $"[AUTH REQUEST] With BasicAuth for {_core.BasicAuthenticationCredential.UserName}");
            // Répondre au challenge Basic
            handler.Proceed(_core.BasicAuthenticationCredential.UserName, _core.BasicAuthenticationCredential.Password);
            return;
        }

        Log.Debug("WEBVIEW", $"[AUTH REQUEST] Canceled");
        // Si tu ne veux pas répondre
        handler.Cancel();
    }
}
