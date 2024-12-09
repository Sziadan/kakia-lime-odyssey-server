using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.SC;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_DISCARD_SLOT_ITEM)]
class CS_DISCARD_SLOT_ITEM_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var cs_slot_item = PacketConverter.Extract<CS_DISCARD_SLOT_ITEM>(p.Payload);
		var inventory = client.GetInventory();

		var item = inventory.AtSlot(cs_slot_item.slot.slot);
		if (item == null)
			return;

		if (item.GetAmount() == 1 && cs_slot_item.count == 1)
		{
			inventory.RemoveItem(cs_slot_item.slot.slot);

			SC_DELETE_SLOT_ITEM sc_delete = new()
			{
				slot = cs_slot_item.slot
			};

			using PacketWriter pw = new(client.GetClientRevision() == 345);
			pw.Write(sc_delete);
			client.Send(pw.ToPacket(), default).Wait();
			return;
		}

	}
}
