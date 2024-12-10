using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets.Packets.SC;
using Newtonsoft.Json;

namespace kakia_lime_odyssey_server.Models;

public class PlayerInventory : IPlayerInventory
{
	// Amount of unique items the player can hold
	public int MaxSize = 96;
	public byte Grade = 1;

	[JsonProperty]
	private Dictionary<int, Item?> _inventory = new();

	public PlayerInventory()
	{
		for(int i =  1; i <= MaxSize; i++)
			_inventory.Add(i, null);
	}

	public int FreeSlotsCount()
	{
		return _inventory.Count(m => m.Value is null);
	}

	public IItem? AtSlot(int slot)
	{
		if (_inventory.ContainsKey(slot))
			return _inventory[slot];
		return null;
	}

	public SC_INVENTORY_ITEM_LIST AsInventoryPacket()
	{
		return new()
		{
			maxCount = MaxSize,
			inventoryGrade = Grade,
			inventory = _inventory
							.Where(kv1 => kv1.Value is not null)
							.Select(kv2 => kv2.Value!.AsInventoryItem(kv2.Key))
							.ToArray()
		};
	}

	/// <summary>
	/// Finds index of first item matching.
	/// Returns -1 if no item found.
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public int FindItem(long id, ulong maxStackSize)
	{
		foreach(var kv in _inventory)
		{
			if (kv.Value is null) continue;
			if (kv.Value.Id == id && kv.Value.Count < maxStackSize)
				return kv.Key;
		}

		return -1;
	}

	public void UpdateItemAtSlot(int slot, IItem item)
	{
		if (!_inventory.ContainsKey(slot)) return;

		if (item is not Item)
			return;

		_inventory[slot] = item as Item;
	}

	public bool RemoveItem(int slot)
	{
		if (!_inventory.ContainsKey(slot))
			return false;

		_inventory[slot] = null;
		return true;
	}

	/// <summary>
	/// If -1, use first free slot
	/// </summary>
	/// <param name="item"></param>
	/// <param name="slot"></param>
	/// <returns></returns>
	public int AddItem(IItem item, int slot = -1)
	{
		if (slot > 0 && _inventory[slot] is not null)
			return -1;
		if (slot > 0 && _inventory[slot] is null)
		{
			_inventory[slot] = item as Item;
			return slot;
		}

		for (int i = 1; i < MaxSize; i++)
		{
			if (_inventory[i] is not null)
				continue;

			_inventory[i] = item as Item;
			return i;
		}

		return -1;
	}
}
