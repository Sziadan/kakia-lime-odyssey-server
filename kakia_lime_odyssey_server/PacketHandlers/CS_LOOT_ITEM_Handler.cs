using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Models;
using kakia_lime_odyssey_server.Network;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_LOOT_ITEM)]
class CS_LOOT_ITEM_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var lootItem = PacketConverter.Extract<CS_LOOT_ITEM>(p.Payload);

		var inventory = client.GetInventory();

		var item = LimeServer.ItemDB.FirstOrDefault(m => m.Id == lootItem.popCount);
		if (item is null)
			return;

		if (!LimeServer.TryGetEntity(client.GetCurrentTarget(), out var entity))
			return;

		var lootTable = entity?.GetLoot();

		// Has already been removed
		// or never had this item to begin with
		if (lootTable is null || !lootTable.Contains(item))
			return;

		int stackIdx = -1;

		if (item.Stackable())		
			stackIdx = inventory.FindItem(item.Id, Item.MaxStackSize);

		if (stackIdx >= 0)
		{
			var currentItem = inventory.AtSlot(stackIdx);
			currentItem?.UpdateAmount(currentItem.GetAmount() + 1);
			inventory.UpdateItemAtSlot(stackIdx, currentItem!);

			SC_UPDATE_SLOT_ITEM sc_update_slot = new()
			{
				count = (long)currentItem!.GetAmount(),
				slot = new ITEM_SLOT()
				{
					slot = stackIdx,
					type = 0
				}
			};

			using (PacketWriter pw = new(client.GetClientRevision() == 345))
			{
				pw.Write(sc_update_slot);
				client.Send(pw.ToPacket(), default).Wait();
			}
		}
		else
		{
			var slot = inventory.AddItem(item);
			SC_INSERT_SLOT_ITEM sc_item = new()
			{
				slot = new()
				{
					type = (byte)0, // 0 == inventory | 1 == bank
					slot = slot
				},
				typeID = item.Id,
				count = 1,
				durability = 200,
				mdurability = 200,
				remainExpiryTime = 0,
				grade = item.Grade,
				inherits = item.GetInherits()
			};

			using (PacketWriter pw = new(client.GetClientRevision() == 345))
			{
				pw.Write(sc_item);
				client.Send(pw.ToPacket(), default).Wait();
			}
		}

		entity.Loot(item);

		SC_LOOTED_ITEM sc_looted = new()
		{
			slot = lootItem.popSlot,
			count = lootItem.popCount
		};

		using (PacketWriter pw = new(client.GetClientRevision() == 345))
		{
			pw.Write(sc_looted);
			client.Send(pw.ToPacket(), default).Wait();
		}

	}
}
