using kakia_lime_odyssey_packets.Packets.Interface;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct SC_TALKING : IPacketVar
{
	public long objInstID;
	public string dialog;
}

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct SC_TALKING_CHOICE : IPacketVar
{
	public long objInstID;
}