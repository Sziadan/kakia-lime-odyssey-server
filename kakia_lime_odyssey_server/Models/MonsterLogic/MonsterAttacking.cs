using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Interfaces;
using kakia_lime_odyssey_server.Network;

namespace kakia_lime_odyssey_server.Models.MonsterLogic;

public partial class Monster : INPC
{
	private bool _startedAttacking = false;

	private void AttackPlayer(uint serverTick, ReadOnlySpan<PlayerClient> playerClients)
	{
		if (CurrentTarget == null)
		{
			ReturnHome(serverTick);
			_currentState = MOB_STATE.ROAMING;
			return;
		}

		var currentPosition = GetMobCurrentPosition(serverTick);
		var distanceToPlayer = CurrentTarget.GetPosition().CalculateDistance(currentPosition);
		if (distanceToPlayer >= 10)
		{
			_currentState = MOB_STATE.CHASING;
			return;
		}

		using (PacketWriter pw = new(false))
		{
			SC_START_COMBATING sc_start_combat = new()
			{
				instID = Id
			};
			pw.Write(sc_start_combat);
			SendToNearbyPlayers(pw.ToPacket(), playerClients);
		}

		if ( !_startedAttacking || (_startedAttacking && (serverTick - _actionStartTick > 2000)))
		{
			_startedAttacking = true;
			_actionStartTick = serverTick;

			using (PacketWriter pw = new(false))
			{
				SC_DO_ACTION actionSkill = new()
				{
					objInstID = Id,
					type = 2140501,
					loopCount = 0
				};
				pw.Write(actionSkill);
				SendToNearbyPlayers(pw.ToPacket(), playerClients);
			}

			var damage = DamageHandler.DealWeaponHitDamage(this, CurrentTarget);
			if (damage.Packet is null)
				return;


			SendToNearbyPlayers(damage.Packet, playerClients);
			var result = CurrentTarget.UpdateHealth((int)(damage.Damage * -1));

			if (result.TargetKilled)
			{
				using (PacketWriter pw = new(false))
				{
					SC_DEAD dead = new()
					{
						objInstID = CurrentTarget.GetObjInstID()						
					};


					pw.Write(dead);
					SendToNearbyPlayers(pw.ToPacket(), playerClients);
				}

				CurrentTarget = null;
				ReturnHome(serverTick);
			}


			/*
			using (PacketWriter pw = new(false))
			{
				SC_USE_SKILL_OBJ_RESULT_LIST actionSkill = new()
				{
					fromInstID = Id,
					toInstID = CurrentTarget.GetObjInstID(),
					typeID = (uint)200002,
					useHP = 0,
					useMP = 0,
					useLP = 0,
					useSP = 0,
					coolTime = 0
				};
				pw.Write(actionSkill);
				SendToNearbyPlayers(pw.ToSizedPacket(), playerClients);
			}
			*/
		}
	}
}
