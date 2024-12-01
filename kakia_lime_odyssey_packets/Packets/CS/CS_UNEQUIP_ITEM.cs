using kakia_lime_odyssey_packets.Packets.Interface;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.CS;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CS_UNEQUIP_ITEM : IPacketFixed
{
	public byte equipSlot;
	public int invSlot;
}
