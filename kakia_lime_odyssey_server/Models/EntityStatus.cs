namespace kakia_lime_odyssey_server.Models;

public class EntityStatus
{
	public byte Lv;
	public ulong Exp;
	public BasicStatus BasicStatus = new();
	public BaseStats BaseStats = new();
	public AttackStatus MeleeAttack = new();
	public AttackStatus SpellAttack = new();
}

public class AttackStatus
{
	public uint WeaponTypeId;
	public ushort Atk;
	public ushort Hit;
	public ushort Def;
	public ushort CritRate;
	public ushort FleeRate;
}

public class BaseStats
{
	public ushort Strength;
	public ushort Intelligence;
	public ushort Dexterity;
	public ushort Agility;
	public ushort Vitality;
	public ushort Spirit;
	public ushort Lucky;
}

public class BasicStatus
{
	public uint Hp;
	public uint MaxHp;
	public uint Mp;
	public uint MaxMp;
}