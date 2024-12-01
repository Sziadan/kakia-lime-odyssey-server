using kakia_lime_odyssey_packets.Packets.Interface;
using kakia_lime_odyssey_packets.Packets.Models;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SC_MOVE : IPacketFixed
{
	public long objInstID;
	public FPOS pos;
	public FPOS dir;
	public float deltaLookAtRadian;
	public uint tick;
	public float velocity;
	public float accel;
	public float turningSpeed;
	public float velocityRatio;
	public byte moveType;
}
