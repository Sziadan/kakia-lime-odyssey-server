using kakia_lime_odyssey_packets.Packets.Models;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct SC_ENTER_ZONEOBJ
{
	public long objInstID;
	public FPOS pos;
	public FPOS dir;
}
