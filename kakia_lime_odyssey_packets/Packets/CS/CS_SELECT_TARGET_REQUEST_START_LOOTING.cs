using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.CS;

[StructLayout(LayoutKind.Sequential, Pack = 2)]
public struct CS_SELECT_TARGET_REQUEST_START_LOOTING
{
	public long objInstID;
}
