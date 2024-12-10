using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct MOVE_SLOT
{
	public ITEM_SLOT fromSlot;
	public ulong fromCount; //MessedUpLELong fromCount;
	public ITEM_SLOT toSlot;
	public ulong toCount; // MessedUpLELong toCount;
}
