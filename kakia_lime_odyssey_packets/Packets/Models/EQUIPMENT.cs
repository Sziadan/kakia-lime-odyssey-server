using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct EQUIPMENT
{
	/// <summary>
	/// EQUIPMENT_TYPE
	/// </summary>
	public byte type;
	public int invSlot;
	public byte equipSlot;
}

public enum EQUIPMENT_TYPE : byte
{
  TYPE_EQUIP = 0x0,
  TYPE_UNEQUIP = 0x1,
  TYPE_WIRED_EQUIP = 0x2,
  TYPE_WIRED_UNEQUIP = 0x3
};