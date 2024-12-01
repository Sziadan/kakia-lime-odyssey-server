using kakia_lime_odyssey_packets.Packets.SC;

namespace kakia_lime_odyssey_network.Interface;

public interface IPlayerInventory
{
	public int FreeSlotsCount();

	public IItem? AtSlot(int slot);

	public SC_INVENTORY_ITEM_LIST AsInventoryPacket();

	public bool RemoveItem(int slot);

	/// <summary>
	/// If -1, use first free slot
	/// </summary>
	/// <param name="item"></param>
	/// <param name="slot"></param>
	/// <returns></returns>
	public int AddItem(IItem item, int slot = -1);
}
