using kakia_lime_odyssey_packets.Packets.Interface;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 2)]
public struct SC_START_CASTING_SKILL_OBJ : IPacketFixed
{
	public long fromInstID;
	public long targetInstID;
	public uint typeID;
	public uint castTime;
}
