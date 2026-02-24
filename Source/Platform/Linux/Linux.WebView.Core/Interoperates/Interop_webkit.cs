namespace Linux.WebView.Core.Interoperates;

public enum WebKitUserContentInjectedFrames
{
    WEBKIT_USER_CONTENT_INJECT_ALL_FRAMES = 0,
    WEBKIT_USER_CONTENT_INJECT_TOP_FRAME = 1
}

public enum WebKitUserScriptInjectionTime
{
    WEBKIT_USER_SCRIPT_INJECT_AT_DOCUMENT_START = 0,
    WEBKIT_USER_SCRIPT_INJECT_AT_DOCUMENT_END = 1
}

public enum WebKitPolicyDecisionType
{
    NavigationAction = 0,
    NewWindowAction = 1,
    Response = 2,
}
