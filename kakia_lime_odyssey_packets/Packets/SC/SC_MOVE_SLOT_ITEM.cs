using kakia_lime_odyssey_packets.Packets.Interface;
using kakia_lime_odyssey_packets.Packets.Models;

namespace kakia_lime_odyssey_packets.Packets.SC;

public struct SC_MOVE_SLOT_ITEM : IPacketVar
{
	public List<MOVE_SLOT> move_list;
}
