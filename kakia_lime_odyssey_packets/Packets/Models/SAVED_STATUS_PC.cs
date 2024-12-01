using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential)]
public struct SAVED_STATUS_PC_KR
{
	public uint hp;
	public uint mp;
	public uint lp;
	public uint streamPoint;
	public uint breath;
	public uint fatigue;
	public SAVED_LIFE_JOB_STATUS lifeJob;
	public SAVED_COMBAT_JOB_STATUS combatJob;
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SAVED_STATUS_PC
{
	public byte Unk;
	public byte lifeJobLv;
	public byte combatJobLv;
	public byte Unk2;
}


public class ModSavedStatus
{
	public uint hp;
	public uint mp;
	public uint lp;
	public uint streamPoint;
	public uint breath;
	public uint fatigue;
	public ModLifeJobStatus lifeJob;
	public ModCombatJobStatus combatJob;

	public ModSavedStatus(SAVED_STATUS_PC_KR status)
	{
		this.hp = status.hp;
		this.mp = status.mp;
		this.lp = status.lp;
		this.streamPoint = status.streamPoint;
		this.breath = status.breath;
		this.fatigue = status.fatigue;
		this.lifeJob = new ModLifeJobStatus(status.lifeJob);
		this.combatJob = new ModCombatJobStatus(status.combatJob);
	}

	public SAVED_STATUS_PC_KR AsStruct()
	{
		return new SAVED_STATUS_PC_KR()
		{
			hp = this.hp,
			mp = this.mp,
			lp = this.lp,
			streamPoint = this.streamPoint,
			breath = this.breath,
			fatigue = this.fatigue,
			lifeJob = lifeJob.AsStruct(),
			combatJob = combatJob.AsStruct()
		};
	}
}