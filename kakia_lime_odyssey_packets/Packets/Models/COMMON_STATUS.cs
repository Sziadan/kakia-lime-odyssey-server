using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential)]
public struct COMMON_STATUS
{
	public byte lv;
	public uint hp;
	public uint mhp;
	public uint mp;
	public uint mmp;
}
