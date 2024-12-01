using kakia_lime_odyssey_packets.Packets.Models;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.CS;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CS_JUMP_PC
{
	public FPOS pos;
	public float deltaLookAtRadian;
	public FPOS dir;
	public uint tick;
	[MarshalAs(UnmanagedType.U1)]
	public bool isSwim;
	public byte dirType;
}
