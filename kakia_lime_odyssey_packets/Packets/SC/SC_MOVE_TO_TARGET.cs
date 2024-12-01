using kakia_lime_odyssey_packets.Packets.Interface;
using kakia_lime_odyssey_packets.Packets.Models;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SC_MOVE_TO_TARGET : IPacketFixed
{
	public long objInstID;
	public FPOS startPos;
	public FPOS targetPos;
	public uint tick;
	public float velocity;
	public int aniId;
	public byte moveType;
}
