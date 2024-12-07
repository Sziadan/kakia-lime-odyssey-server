using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Models;
using kakia_lime_odyssey_server.Network;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_SELECT_TARGET_REQUEST_START_LOOTING)]
class CS_SELECT_TARGET_REQUEST_START_LOOTING_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var monsterId =  client.GetCurrentTarget();
		if (monsterId == 0) return;

		if (!LimeServer.TryGetEntity(monsterId, out var mob)) return;


		List<INVENTORY_ITEM> items = new();

		var loot = mob.GetLoot();
		for (int i = 0; i < loot.Count; i++)
			items.Add(loot[i].AsInventoryItem(i + 1));

		SC_LOOTABLE_ITEM_LIST sc_lootable_item_list = new() 
		{ 
			count = (ushort)items.Count,
			lootTable = items.ToArray()
		};

		using PacketWriter pw = new PacketWriter(client.GetClientRevision() == 345);
		pw.Write(sc_lootable_item_list);

		client.Send(pw.ToSizedPacket(), default).Wait();
	}
}
