using kakia_lime_odyssey_packets.Packets.Interface;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.CS;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CS_EQUIP_ITEM : IPacketFixed
{
	public int invSlot;
	public byte equipSlot;
}
