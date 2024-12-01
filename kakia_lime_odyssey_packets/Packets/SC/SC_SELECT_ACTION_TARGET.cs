using kakia_lime_odyssey_packets.Packets.Interface;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 2)]
public struct SC_SELECT_ACTION_TARGET : IPacketFixed
{
	public long objInstID;
	public long targetInstID;
}
