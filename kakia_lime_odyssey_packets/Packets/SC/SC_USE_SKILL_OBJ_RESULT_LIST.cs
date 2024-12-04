using kakia_lime_odyssey_packets.Packets.Interface;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct SC_USE_SKILL_OBJ_RESULT_LIST : IPacketVar
{
	public long fromInstID;
	public long toInstID;
	public uint typeID;
	public short useHP;
	public short useMP;
	public short useSP;
	public short useLP;
	public uint coolTime;
}
