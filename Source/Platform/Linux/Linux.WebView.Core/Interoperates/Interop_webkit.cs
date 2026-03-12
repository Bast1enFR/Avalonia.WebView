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

[Flags]
public enum WebKitWebsiteDataTypes
{
    MemoryCache = 1 << 0,
    DiskCache = 1 << 1,
    OfflineApplicationCache = 1 << 2,
    SessionStorage = 1 << 3,
    LocalStorage = 1 << 4,
    IndexeddbDatabases = 1 << 5,
    WebsqlDatabases = 1 << 6,
    Cookies = 1 << 7,
    DeviceIdHashSalt = 1 << 8,
    HstsCache = 1 << 9,
    Itp = 1 << 10,
    ServiceWorkerRegistrations = 1 << 11,
    DomCache = 1 << 12,
    All = (1 << 13) - 1
}

public enum WebKitSnapshotRegion : uint
{
    Visible = 0,
    FullDocument = 1
}

public enum WebKitSnapshotOptions : uint
{
    None = 0
}