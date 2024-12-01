using kakia_lime_odyssey_packets.Packets.Interface;
using System;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 2)]
public struct SC_DO_ACTION : IPacketFixed
{
	public ulong objInstID;
	public uint type;
	public float loopCount;
}
