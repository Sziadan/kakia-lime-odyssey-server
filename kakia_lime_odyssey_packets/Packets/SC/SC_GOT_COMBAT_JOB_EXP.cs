using kakia_lime_odyssey_packets.Packets.Interface;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 2)]
public struct SC_GOT_COMBAT_JOB_EXP : IPacketFixed
{
	public uint exp;
	public uint addExp;
}
