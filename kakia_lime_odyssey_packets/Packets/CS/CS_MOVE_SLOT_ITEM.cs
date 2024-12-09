using kakia_lime_odyssey_packets.Packets.Interface;
using kakia_lime_odyssey_packets.Packets.Models;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.CS;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct CS_MOVE_SLOT_ITEM : IPacketFixed
{
	public ITEM_SLOT from;
	public ITEM_SLOT to;
	public ushort count;

	// Just garbage?
	public ushort temp1;
	public ushort temp2;
	public ushort temp3;
}
