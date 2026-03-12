using Linux.WebView.Core;

namespace Avalonia.WebView.Linux.Core;

partial class LinuxWebViewCore
{
    private bool WebView_PermissionRequest(nint pWebView, nint pPermissionRequest, nint pArg)
    {
        GtkApi.PermissionRequestAllow(pPermissionRequest);
        return true;
    }

    bool WebView_DecidePolicy(nint pWebView, nint pPolicyDecision, WebKitPolicyDecisionType type)
    {
        if (type == WebKitPolicyDecisionType.Response)
            return false;

        var pRequest = GtkApi.NavigationPolicyDecisionGetRequest(pPolicyDecision);

        if (pRequest == IntPtr.Zero)
        {
            GtkApi.IgnorePolicyDecision(pPolicyDecision);
            return false;
        }

        var uriString = GtkApi.UriRequestGetUri(pRequest);
        var uri = new Uri(uriString);

        WebViewUrlLoadingEventArg args = new () 
        { 
            Url = uri, 
            RawArgs = pPolicyDecision 
        };

        bool isSucceed = false;

        try
        {
            _callBack.PlatformWebViewNavigationStarting(this, args);
            if (args.Cancel)
            {
                GtkApi.IgnorePolicyDecision(pPolicyDecision);
                return false;
            }

            if (_webScheme?.BaseUri.IsBaseOf(uri) == true)
            {
                GtkApi.UsePolicyDecision(pPolicyDecision);
                isSucceed = true;
                return true;
            }

            var newWindowEventArgs = new WebViewNewWindowEventArgs()
            {
                Url = uri,
                UrlLoadingStrategy = type == WebKitPolicyDecisionType.NewWindowAction ? UrlRequestStrategy.OpenExternally : UrlRequestStrategy.OpenInWebView
            };

            if (!_callBack.PlatformWebViewNewWindowRequest(this, newWindowEventArgs))
            {
                GtkApi.IgnorePolicyDecision(pPolicyDecision);
                return false;
            }

            switch (newWindowEventArgs.UrlLoadingStrategy)
            {
                case UrlRequestStrategy.OpenExternally:
                case UrlRequestStrategy.OpenInNewWindow:
                    OpenUriHelper.OpenInProcess(uri);
                    GtkApi.IgnorePolicyDecision(pPolicyDecision);
                    isSucceed = true;
                    break;
                case UrlRequestStrategy.OpenInWebView:
                    GtkApi.UsePolicyDecision(pPolicyDecision);
                    isSucceed = true;
                    break;
                case UrlRequestStrategy.CancelLoad:
                default:
                    GtkApi.IgnorePolicyDecision(pPolicyDecision);
                    return false;
            }
        }
        catch (Exception)
        {
            isSucceed = false;
        }
        _callBack.PlatformWebViewNavigationCompleted(this, new WebViewUrlLoadedEventArg() { IsSuccess = isSucceed, RawArgs = pPolicyDecision });
        return true;
    }
}
