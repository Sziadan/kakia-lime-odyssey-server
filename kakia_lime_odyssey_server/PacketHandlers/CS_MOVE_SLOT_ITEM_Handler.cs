using kakia_lime_odyssey_logging;
using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Models;
using kakia_lime_odyssey_utils.Extensions;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_MOVE_SLOT_ITEM)]
class CS_MOVE_SLOT_ITEM_Handler : PacketHandler
{
	public bool ShouldStack(IItem item1, IItem item2)
	{
		return item1.GetId() == item2.GetId();
	}

	public void CreateStacks(IItem item1, IItem item2)
	{
		var totalAmount = item1.GetAmount() + item2.GetAmount();
		if (totalAmount <= Item.MaxStackSize)
		{
			item1.UpdateAmount(0);
			item2.UpdateAmount(totalAmount);
			return;
		}
		else
		{
			item1.UpdateAmount(totalAmount - Item.MaxStackSize);
			item2.UpdateAmount(99);
		}
	}

	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var cs_move = PacketConverter.Extract<CS_MOVE_SLOT_ITEM>(p.Payload);
		var inventory = client.GetInventory();

		var item_slot1 = inventory.AtSlot(cs_move.from.slot);
		var item_slot2 = inventory.AtSlot(cs_move.to.slot);

		SC_MOVE_SLOT_ITEM sc_move = new()
		{
			move_list = new()
		};


		// TODO:
		// Not the proper way to update stacks
		// haven't figured out yet why moving
		// increased amounts don't work though.
		SC_UPDATE_SLOT_ITEM? sc_update_slot = null;

		// Moving item to empty slot
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
		// Moving item to slot where there already is an item
		else if (item_slot1 is not null && item_slot2 is not null)
		{
			if (ShouldStack(item_slot1, item_slot2))
			{
				ulong prevCount = item_slot2.GetAmount();


				CreateStacks(item_slot1, item_slot2);
				if (item_slot1.GetAmount() == 0)
					inventory.RemoveItem(cs_move.from.slot);
				else
					inventory.UpdateItemAtSlot(cs_move.from.slot, item_slot1);

				inventory.UpdateItemAtSlot(cs_move.to.slot, item_slot2);

				sc_move.move_list.Add(new()
				{
					fromCount = cs_move.count - item_slot1.GetAmount(),
					toCount = item_slot2.GetAmount(),
					fromSlot = cs_move.from,
					toSlot = cs_move.to
				});


				// TODO:
				// Not the proper way to update stacks
				// haven't figured out yet why moving
				// increased amounts don't work though.
				sc_update_slot = new()
				{
					count = (long)item_slot2.GetAmount(),
					slot = cs_move.to
				};
			}
			else
			{
				// Regular switch-a-roo of items
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

				sc_move.move_list.Add(new()
				{
					fromCount = cs_move.count,
					toCount = cs_move.count,
					fromSlot = cs_move.to,
					toSlot = cs_move.from
				});
			}
		}
		else return; // Moving no item at all.. Shouldn't be a thing, right?

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(sc_move);

		Logger.Log(pw.ToSizedPacket().ToFormatedHexString());
		client.Send(pw.ToSizedPacket(), default).Wait();


		// TODO:
		// Not the proper way to update stacks
		// haven't figured out yet why moving
		// increased amounts don't work though.
		if (!sc_update_slot.HasValue)
			return;

		using PacketWriter pw2 = new(client.GetClientRevision() == 345);
		pw2.Write(sc_update_slot);
		client.Send(pw2.ToPacket(), default).Wait();
	}
}
