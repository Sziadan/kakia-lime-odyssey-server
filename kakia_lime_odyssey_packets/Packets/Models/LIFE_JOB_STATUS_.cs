using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct LIFE_JOB_STATUS_
{
	public byte lv;
	public uint exp;
	public ushort statusPoint;
	public ushort idea;
	public ushort mind;
	public ushort craft;
	public ushort sense;
}
