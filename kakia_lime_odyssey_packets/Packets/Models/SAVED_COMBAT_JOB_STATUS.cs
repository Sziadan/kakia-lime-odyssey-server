using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct SAVED_COMBAT_JOB_STATUS
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

public class ModCombatJobStatus
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

	public ModCombatJobStatus(SAVED_COMBAT_JOB_STATUS status)
	{
		lv = status.lv;
		exp = status.exp;
		strength = status.strength;
		intelligence = status.intelligence;
		dexterity = status.dexterity;
		agility = status.agility;
		vitality = status.vitality;
		spirit = status.spirit;
		lucky = status.lucky;
	}

	public SAVED_COMBAT_JOB_STATUS AsStruct()
	{
		return new SAVED_COMBAT_JOB_STATUS()
		{
			lv = lv,
			exp = exp,
			strength = strength,
			intelligence = intelligence,
			dexterity = dexterity,
			agility = agility,
			vitality = vitality,
			spirit = spirit,
			lucky = lucky
		};
	}
}