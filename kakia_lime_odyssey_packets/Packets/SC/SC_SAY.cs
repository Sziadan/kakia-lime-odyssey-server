using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct SC_SAY
{
	public long objInstID;
	public uint maintainTime;
	public int type;
	public string message;
}