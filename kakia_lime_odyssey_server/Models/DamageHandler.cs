using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_server.Interfaces;
using kakia_lime_odyssey_packets.Packets.Enums;

namespace kakia_lime_odyssey_server.Models;

public static class DamageHandler
{
	public static DamageDealtResult DealWeaponHitDamage(IEntity source, IEntity target)
	{
		var sourceStatus = source.GetEntityStatus();
		var targetStatus = target.GetEntityStatus();

		Random rnd = new Random();
		double varianceBonus = 1 + rnd.NextDouble();
		bool isCrit = rnd.Next(0, 100) <= sourceStatus.MeleeAttack.CritRate;
		if (!isCrit)
			varianceBonus = 2;
		else if (varianceBonus > 1.5) // Don't go above 1.5 if not a crit
			varianceBonus = 1.5;

		uint damage = 0;
		bool isMiss = targetStatus.MeleeAttack.FleeRate == 0 ? false : rnd.Next(0, 100) >= ((sourceStatus.MeleeAttack.Hit / (double)targetStatus.MeleeAttack.FleeRate) * 100);

		if (isMiss) damage = 0;
		else if (isCrit)
		{
			damage = (uint)(sourceStatus.MeleeAttack.Atk * varianceBonus);			
		}
		else
		{
			damage = (uint)((sourceStatus.MeleeAttack.Atk - targetStatus.MeleeAttack.Def) * varianceBonus);
		}

		using (PacketWriter pw = new(false))
		{
			SC_WEAPON_HIT_RESULT hit = new()
			{
				fromInstID = source.GetId(),
				targetInstID = target.GetId(),
				glared = isCrit,
				aniSpeedRatio = 1,
				main = new()
				{
					result = (byte)(isMiss ? HIT_FAIL_TYPE.HIT_FAIL_MISS : HIT_FAIL_TYPE.HIT_FAIL_HIT),
					weaponTypeID = (int)sourceStatus.MeleeAttack.WeaponTypeId,
					damage = damage,
					bonusDamage = 0
				},
				sub = new()
				{
					result = 0,
					weaponTypeID = 0,
					damage = 0,
					bonusDamage = 0
				},
				ranged = false,
				rangeHitDelay = 0,
				rangedVelocity = 0
			};


			pw.Write(hit);

			return new DamageDealtResult() 
			{ 
				Damage = damage,
				Packet = pw.ToPacket()
			};
		}
	}
}

public class DamageDealtResult
{
	public uint Damage;
	public byte[]? Packet;
}
