using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.CS;

[StructLayout(LayoutKind.Sequential, Pack = 2)]
public struct CS_SELECT_TARGET_READY_WEAPON_HITING
{
	public long targetInstID;
}
