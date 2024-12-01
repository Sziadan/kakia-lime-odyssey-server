using kakia_lime_odyssey_logging;
using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.Enums;
using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Models;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_COMBAT_JOB_EQUIP_ITEM)]
class CS_COMBAT_JOB_EQUIP_ITEM_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var equip_item = PacketConverter.Extract<CS_EQUIP_ITEM>(p.Payload);
		var inventory = client.GetInventory();
		var equipment = client.GetEquipment(true);

		var item = inventory.AtSlot(equip_item.invSlot);
		if (item is null)
		{
			Logger.Log($"Trying to equip item that user doesn't have? Slot: {equip_item.invSlot}", LogLevel.Error);
			return;
		}

		if (!inventory.RemoveItem(equip_item.invSlot))
			Logger.Log($"Failed to remove item from slot {equip_item.invSlot} in inventory.", LogLevel.Error);

		List<EQUIPMENT> equipActions = new();
		var eq_slot = (EQUIP_SLOT)equip_item.equipSlot;

		if (equipment.IsEquipped(eq_slot))
		{
			Item prev = (equipment.UnEquip(eq_slot) as Item)!;
			int new_inv_slot = inventory.AddItem(prev, equip_item.invSlot);
			
			equipActions.Add(prev.EquipChangePart(EQUIPMENT_TYPE.TYPE_UNEQUIP, new_inv_slot));
		}

		equipment.Equip(eq_slot, item);
		equipActions.Add((item as Item)!.EquipChangePart(EQUIPMENT_TYPE.TYPE_EQUIP, equip_item.invSlot));

		SC_COMBAT_JOB_EQUIPMENT_LIST sc_cmb_equip = new()
		{
			equips = equipActions.ToArray()
		};

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(sc_cmb_equip);
		client.Send(pw.ToSizedPacket(), default).Wait();
		client.SetEquipToCurrentJob();


		// Show the changed appearance!
		SC_UPDATED_APPEARANCE_PC sc_update_appearance = new()
		{
			objInstID = client.GetObjInstID(),
			appearance = client.GetCurrentCharacter().appearance.AsStruct()
		};

		using PacketWriter pw2 = new(client.GetClientRevision() == 345);
		pw2.Write(sc_update_appearance);

		// client doesn't need it, only others do
		client.SendGlobalPacket(pw2.ToPacket(), default).Wait();
	}
}
