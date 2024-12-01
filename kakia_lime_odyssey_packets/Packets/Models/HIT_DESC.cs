using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential)]
public struct HIT_DESC
{
	public byte result;
	public int weaponTypeID;
	public uint damage;
	public uint bonusDamage;
}
