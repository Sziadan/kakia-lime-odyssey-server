using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.CS;

[StructLayout(LayoutKind.Sequential)]
public struct CS_SAY_PC
{
	public uint maintainTime;
	public int type;
	public string message;
}
