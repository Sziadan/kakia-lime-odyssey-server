using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential, Pack = 2)]
public struct MOVE_SLOT
{
	public ITEM_SLOT fromSlot;
	public long fromCount;
	public ITEM_SLOT toSlot;
	public long toCount;
}
