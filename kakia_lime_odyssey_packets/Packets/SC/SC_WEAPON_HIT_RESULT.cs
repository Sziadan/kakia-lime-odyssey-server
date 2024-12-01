using kakia_lime_odyssey_packets.Packets.Interface;
using kakia_lime_odyssey_packets.Packets.Models;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SC_WEAPON_HIT_RESULT : IPacketFixed
{
	public long fromInstID;
	public long targetInstID;
	[MarshalAs(UnmanagedType.U1)]
	public bool glared;
	public float aniSpeedRatio;
	public HIT_DESC main;
	public HIT_DESC sub;
	[MarshalAs(UnmanagedType.U1)]
	public bool ranged;
	public uint rangeHitDelay;
	public float rangedVelocity;
}
