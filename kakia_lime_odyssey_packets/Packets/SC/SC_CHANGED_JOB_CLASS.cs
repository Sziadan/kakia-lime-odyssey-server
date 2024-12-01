using kakia_lime_odyssey_packets.Packets.Interface;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SC_CHANGED_JOB_CLASS : IPacketFixed
{
	public long instID;
	public byte jobClass;
}
