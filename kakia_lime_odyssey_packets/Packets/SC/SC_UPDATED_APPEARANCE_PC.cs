using kakia_lime_odyssey_packets.Packets.Interface;
using kakia_lime_odyssey_packets.Packets.Models;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 2)]
public struct SC_UPDATED_APPEARANCE_PC : IPacketFixed
{
	public ulong objInstID;
	public APPEARANCE_PC_KR appearance;
}
