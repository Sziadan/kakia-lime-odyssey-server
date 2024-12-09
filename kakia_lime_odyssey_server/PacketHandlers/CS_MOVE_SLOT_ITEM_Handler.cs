using kakia_lime_odyssey_logging;
using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_utils.Extensions;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_MOVE_SLOT_ITEM)]
class CS_MOVE_SLOT_ITEM_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var cs_move = PacketConverter.Extract<CS_MOVE_SLOT_ITEM>(p.Payload);
		string hex = p.Payload.ToFormatedHexString();
		Logger.Log(hex);

		var inventory = client.GetInventory();

		var item_slot1 = inventory.AtSlot(cs_move.from.slot);
		var item_slot2 = inventory.AtSlot(cs_move.to.slot);

		SC_MOVE_SLOT_ITEM sc_move = new()
		{
			move_list = new()
		};

		if (item_slot2 is null && item_slot1 is not null)
		{
			inventory.AddItem(item_slot1, cs_move.to.slot);
			inventory.RemoveItem(cs_move.from.slot);

			sc_move.move_list.Add(new()
			{
				fromCount = cs_move.count,
				toCount = cs_move.count,
				fromSlot = cs_move.from,
				toSlot = cs_move.to
			});
		}
		else if (item_slot1 is not null && item_slot2 is not null)
		{
			inventory.RemoveItem(cs_move.to.slot);
			inventory.RemoveItem(cs_move.from.slot);

			inventory.AddItem(item_slot1, cs_move.to.slot);
			inventory.AddItem(item_slot2, cs_move.from.slot);

			sc_move.move_list.Add(new()
			{
				fromCount = cs_move.count,
				toCount = cs_move.count,
				fromSlot = cs_move.from,
				toSlot = cs_move.to
			});
		}
		else return;

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(sc_move);

		client.Send(pw.ToSizedPacket(), default).Wait();
	}
}
