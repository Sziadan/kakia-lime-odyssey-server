using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.CS;

[StructLayout(LayoutKind.Sequential, Pack = 2)]
public struct CS_LOOT_ITEM
{
	public int popSlot;
	public long popCount;
	public int pushSlot;
}
