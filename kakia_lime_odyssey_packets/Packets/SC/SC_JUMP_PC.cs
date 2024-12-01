using kakia_lime_odyssey_packets.Packets.Interface;
using kakia_lime_odyssey_packets.Packets.Models;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SC_JUMP_PC : IPacketFixed
{
	public long objInstID;
	public FPOS pos;
	public FPOS dir;
	public float deltaLookAtRadian;
	public uint tick;
	public float velocity;
	public float accel;
	public float ratio;
	[MarshalAs(UnmanagedType.U1)]
	public bool isSwim;
}
