using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct ITEM_INHERIT
{
	public uint typeID;
	public int value;
	public byte type;
}

[StructLayout(LayoutKind.Sequential)]
public struct ITEM_INHERITS
{
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
	public ITEM_INHERIT[] inherits;
}
