using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential)]
public struct EQUIP_ITEM
{
	public int itemTypeID;
	public byte equipSlot;
	public byte wiredSlot;
	public int durability;
	public int mdurability;
	public int grade;
	public ITEM_INHERITS inherits;
}
