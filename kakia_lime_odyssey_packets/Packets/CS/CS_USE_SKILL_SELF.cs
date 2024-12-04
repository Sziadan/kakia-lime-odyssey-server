using kakia_lime_odyssey_packets.Packets.Interface;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.CS;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CS_USE_SKILL_SELF : IPacketFixed
{
	public uint typeID;
	[MarshalAs(UnmanagedType.U1)]
	public bool combo;
}