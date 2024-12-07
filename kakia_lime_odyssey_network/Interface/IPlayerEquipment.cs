using kakia_lime_odyssey_packets.Packets.Enums;
using kakia_lime_odyssey_packets.Packets.SC;

namespace kakia_lime_odyssey_network.Interface;

public interface IPlayerEquipment
{
	public bool IsEquipped(EQUIP_SLOT slot);
	public IItem? UnEquip(EQUIP_SLOT slot);
	public bool Equip(EQUIP_SLOT slot, IItem item);
	public SC_COMBAT_JOB_EQUIP_ITEM_LIST GetCombatEquipList();
	public IItem? GetItemInSlot(EQUIP_SLOT slot); 
}
