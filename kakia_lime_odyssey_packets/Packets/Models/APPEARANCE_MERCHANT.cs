using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct APPEARANCE_MERCHANT
{
	public APPEARANCE_NPC appearance;
	public short specialistType;
}
