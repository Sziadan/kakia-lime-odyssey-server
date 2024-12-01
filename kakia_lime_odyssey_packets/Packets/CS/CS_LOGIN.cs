using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.CS;

[StructLayout(LayoutKind.Sequential, Pack = 2)]
public struct CS_LOGIN
{
	public int key;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 26)]
	public string id;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 26)]
	public string pw;
	public int revision;
}
