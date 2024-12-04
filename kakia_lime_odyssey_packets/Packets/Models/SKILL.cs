using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential)]
public struct SKILL
{
	public uint typeID;
	public ushort level;
	public uint remainCoolTime;
}
