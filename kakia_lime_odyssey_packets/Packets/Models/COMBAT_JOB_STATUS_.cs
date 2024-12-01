using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct COMBAT_JOB_STATUS_
{
	public byte lv;
	public uint exp;
	public ushort strength;
	public ushort intelligence;
	public ushort dexterity;
	public ushort agility;
	public ushort vitality;
	public ushort spirit;
	public ushort lucky;
}
