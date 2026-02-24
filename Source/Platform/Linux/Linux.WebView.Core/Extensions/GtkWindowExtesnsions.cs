namespace Linux.WebView.Core.Extensions;

public static class GtkWindowExtesnsions
{
    public static nint X11Handle(this nint widgetHandle) => GtkApi.GetWidgetXid(widgetHandle);
}
