using kakia_lime_odyssey_packets.Packets.Interface;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SC_PC_COMBAT_JOB_LEVEL_UP : IPacketFixed
{
	public long objInstID;
	public byte lv;
	public uint exp;
	public ushort newStr;
	public ushort newAgi;
	public ushort newVit;
	public ushort newInl;
	public ushort newDex;
	public ushort newSpi;
}