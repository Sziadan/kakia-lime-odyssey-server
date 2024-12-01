using kakia_lime_odyssey_packets.Packets.Models;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.CS;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CS_STOP_PC
{
	public FPOS pos;
	public FPOS dir;
	public uint tick;
	public byte stopType;
}
