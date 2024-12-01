using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential)]
public struct STATUS_PC
{
	public COMMON_STATUS commonStatus;
	public uint lp;
	public uint mlp;
	public uint streamPoint;
	public float meleeHitRate;
	public float dodge;
	public uint meleeAtk;
	public uint meleeDefense;
	public uint spellAtk;
	public uint spellDefense;
	public float parry;
	public float block;
	public uint resist;
	public float criticalRate;
	public float hitSpeedRatio;
	public LIFE_JOB_STATUS_ lifeJob;
	public COMBAT_JOB_STATUS_ combatJob;
	public VELOCITIES velocities;
	public float collectSucessRate;
	public float collectionIncreaseRate;
	public float makeTimeDecreaseAmount;
}
