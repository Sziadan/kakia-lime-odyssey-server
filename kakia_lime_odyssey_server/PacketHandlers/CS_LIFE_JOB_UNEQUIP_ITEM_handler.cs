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

[PacketHandlerAttr(PacketType.CS_LIFE_JOB_UNEQUIP_ITEM)]
class CS_LIFE_JOB_UNEQUIP_ITEM_handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var cs_unequip = PacketConverter.Extract<CS_UNEQUIP_ITEM>(p.Payload);
		var inventory = client.GetInventory();
		var equipment = client.GetEquipment(true);

		if (inventory.FreeSlotsCount() <= 0)
		{
			// NOT ENOUGH ROOM TO UNEQUIP
			return;
		}

		var item = equipment.UnEquip((EQUIP_SLOT)cs_unequip.equipSlot);
		var slot = inventory.AddItem(item!);

		List<EQUIPMENT> equipActions = new();
		equipActions.Add((item as Item)!.EquipChangePart(EQUIPMENT_TYPE.TYPE_UNEQUIP, slot));

		SC_LIFE_JOB_EQUIPMENT_LIST sc_cmb_equip = new()
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
