using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct INVENTORY_ITEM
{
	public int slot;
	public int typeID;
	public long count;
	public int durability;
	public int mdurability;
	public int remainExpiryTime;
	public int grade;
	public ITEM_INHERITS inherits;
}
