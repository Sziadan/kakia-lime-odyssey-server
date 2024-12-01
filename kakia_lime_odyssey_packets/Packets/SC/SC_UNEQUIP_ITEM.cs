using kakia_lime_odyssey_packets.Packets.Interface;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SC_COMBAT_JOB_UNEQUIP_ITEM : IPacketFixed
{
	public byte equipSlot;
	public int invSlot;
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SC_LIFE_JOB_UNEQUIP_ITEM : IPacketFixed
{
	public byte equipSlot;
	public int invSlot;
}