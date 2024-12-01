using kakia_lime_odyssey_packets.Packets.Interface;
using System.Runtime.InteropServices;
namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 2)]
public struct SC_START_CONTINUOUS_ACTION : IPacketFixed
{
	public ulong instID;
	public uint action;
}
