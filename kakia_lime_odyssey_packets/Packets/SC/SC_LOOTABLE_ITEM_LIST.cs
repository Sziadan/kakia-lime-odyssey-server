using kakia_lime_odyssey_packets.Packets.Interface;
using kakia_lime_odyssey_packets.Packets.Models;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

public struct SC_LOOTABLE_ITEM_LIST : IPacketVar
{
	public ushort count;
	public INVENTORY_ITEM[] lootTable;
}
