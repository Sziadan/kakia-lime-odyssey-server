using kakia_lime_odyssey_packets.Packets.Models;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct SC_COMBAT_JOB_EQUIPMENT_LIST
{
	public EQUIPMENT[] equips;
}

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct SC_LIFE_JOB_EQUIPMENT_LIST
{
	public EQUIPMENT[] equips;
}