using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential)]
public struct APPEARANCE_NPC
{
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
	public string name;
	public uint action;
	public uint actionStartTick;
	public float scale;
	public float transparent;
	public uint modelTypeID;
	public COLOR color;
	public uint typeID;
}
