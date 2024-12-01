using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets.Packets.Enums;
using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;
using Newtonsoft.Json;

namespace kakia_lime_odyssey_server.Models;

public class PlayerEquipment : IPlayerEquipment
{
	[JsonProperty]
	private Dictionary<EQUIP_SLOT, Item?> equipment = new();

	public PlayerEquipment()
	{
		for(int i = 1; i <= 20; i++)
		{
			equipment.Add((EQUIP_SLOT)i, null);
		}
	}

	public bool IsEquipped(EQUIP_SLOT slot)
	{
		if (equipment.ContainsKey(slot))
			return equipment[slot] is not null;
		return false;
	}

	public IItem? UnEquip(EQUIP_SLOT slot)
	{
		if (!equipment.ContainsKey(slot)) 
			return null;

		var item = equipment[slot];
		equipment[slot] = null;
		return item;
	}

	public bool Equip(EQUIP_SLOT slot, IItem item)
	{
		if (!equipment.ContainsKey(slot))
			return false;
		if (equipment[slot] is not null)
			return false;

		equipment[slot] = item as Item;
		return true;
	}

	public SC_COMBAT_JOB_EQUIP_ITEM_LIST GetCombatEquipList()
	{
		return new()
		{
			equipList = equipment
						.Values
						.Where(e => e is not null)
						.Select(m => m!.AsEquipItem())
						.ToArray()
		};
	}

	private int GetSlotID(EQUIP_SLOT slot)
	{
		return equipment[slot] is null ? 0 : equipment[slot]!.Id;
	}

	public EQUIPPED GetEquipped()
	{
		var equip = new EQUIPPED()
		{
			MAIN_EQUIP = GetSlotID(EQUIP_SLOT.MAIN_EQUIP),
			SUB_EQUIP = GetSlotID(EQUIP_SLOT.SUB_EQUIP),
			RANGE_MAIN_EQUIP = GetSlotID(EQUIP_SLOT.RANGE_MAIN_EQUIP),
			SPENDING = GetSlotID(EQUIP_SLOT.SPENDING),
			HEAD = GetSlotID(EQUIP_SLOT.HEAD),
			FOREHEAD = GetSlotID(EQUIP_SLOT.FOREHEAD),
			EYE = GetSlotID(EQUIP_SLOT.EYE),
			MOUTH = GetSlotID(EQUIP_SLOT.MOUTH),
			NECK = GetSlotID(EQUIP_SLOT.NECK),
			SHOULDER = GetSlotID(EQUIP_SLOT.SHOULDER),
			UPPER_BODY = GetSlotID(EQUIP_SLOT.UPPER_BODY),
			HAND = GetSlotID(EQUIP_SLOT.HAND),
			WAIST = GetSlotID(EQUIP_SLOT.WAIST),
			LOWER_BODY = GetSlotID(EQUIP_SLOT.LOWER_BODY),
			FOOT = GetSlotID(EQUIP_SLOT.FOOT),
			RELIC = GetSlotID(EQUIP_SLOT.RELIC),
			RING_1 = GetSlotID(EQUIP_SLOT.RING_1),
			RING_2 = GetSlotID(EQUIP_SLOT.RING_2),
			ACCESSORY_1 = GetSlotID(EQUIP_SLOT.ACCESSORY_1)
		};

		return equip;
	}
}
