namespace Linux.WebView.Core.Interoperates;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct GSList
{
    public readonly nint Data;
    public readonly GSList* Next;
}