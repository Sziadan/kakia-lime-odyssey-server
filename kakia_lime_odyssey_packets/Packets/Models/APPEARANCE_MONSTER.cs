using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential, Pack =	4)]
public struct APPEARANCE_MONSTER
{
	public APPEARANCE_NPC appearance;
	[MarshalAs(UnmanagedType.U1)]
	public bool aggresive;
	[MarshalAs(UnmanagedType.U1)]
	public bool shineWhenHitted;
}
