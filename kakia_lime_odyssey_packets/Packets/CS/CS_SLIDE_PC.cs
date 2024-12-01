using kakia_lime_odyssey_packets.Packets.Models;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.CS;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CS_SLIDE_PC
{
	public FPOS pos;
	public float deltaLookAtRadian;
	public FPOS dir;
	public uint tick;
	public float turningSpeed;
	public byte moveType;
	public float deltaBodyRadian;
	public float velDecRatio;
}
