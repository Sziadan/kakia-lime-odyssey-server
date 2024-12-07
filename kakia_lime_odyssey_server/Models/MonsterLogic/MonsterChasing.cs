using kakia_lime_odyssey_logging;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.Enums;
using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Network;

namespace kakia_lime_odyssey_server.Models.MonsterLogic;

public partial class Monster : INPC
{
	private void ChasePlayer(uint serverTick, ReadOnlySpan<PlayerClient> playerClients)
	{
		if (CurrentTarget != null)
		{
			var currentPosition = GetMobCurrentPosition(serverTick);
			var distanceToPlayer = CurrentTarget.GetPosition().CalculateDistance(currentPosition);
			var distanceFromHome = _originalPosition.CalculateDistance(currentPosition);

			//Logger.Log($"Current Distance: {distanceToPlayer}");

			if (distanceToPlayer > aggro_range || distanceFromHome > 250)
			{
				var stop = GetStopPacket(GetMobCurrentPosition(serverTick), serverTick);
				Position = GetMobCurrentPosition(serverTick);
				SendToNearbyPlayers(stop, playerClients);
				ReturnHome(serverTick);
				return;
			}

			_moveType = MOVE_TYPE.MOVE_TYPE_RUN;

			bool playerMoved = !_destination.Compare(CurrentTarget.GetPosition());

			if (playerMoved && distanceToPlayer >= 10)
			{
				var stop = GetStopPacket(GetMobCurrentPosition(serverTick), serverTick);
				Position = GetMobCurrentPosition(serverTick);
				SendToNearbyPlayers(stop, playerClients);
				IsMoving = false;
				SetNewDestination(CurrentTarget.GetPosition(), serverTick);				
				//MoveToTarget(serverTick, playerClients);				
			}
			if (distanceToPlayer < 10)
			{
				Position = currentPosition;
				IsMoving = false;
				_actionStartTick = serverTick;
				_destination = default;
				var stop = GetStopPacket(GetMobCurrentPosition(serverTick), serverTick);
				Position = GetMobCurrentPosition(serverTick);
				SendToNearbyPlayers(stop, playerClients);
				_currentState = MOB_STATE.ATTACKING;
				_startedAttacking = false;
			}
			else
				MoveTowardsDestination(serverTick, playerClients, 5);

			if (distanceToPlayer > 0)
			{
				var dir = currentPosition.CalculateDirection(CurrentTarget.GetPosition());
				if (!dir.IsNaN())
				{
					Direction = dir;
				}
			}
		}
		else
		{
			var stop = GetStopPacket(GetMobCurrentPosition(serverTick), serverTick);
			Position = GetMobCurrentPosition(serverTick);
			SendToNearbyPlayers(stop, playerClients);
			ReturnHome(serverTick);
		}
	}

	private void MoveToTarget(uint serverTick, ReadOnlySpan<PlayerClient> playerClients)
	{
		FPOS start = GetMobCurrentPosition(serverTick);
		FPOS end = CurrentTarget!.GetPosition();

		if (start.IsNaN() || end.IsNaN())
			return;

		if (start.Compare(end))
			return;

		IsMoving = true;

		SC_MOVE_TO_TARGET moveToTarget = new()
		{
			objInstID = Id,
			startPos = GetMobCurrentPosition(serverTick),
			targetPos = _destination,
			tick = LimeServer.GetCurrentTick(),
			velocity = GetCurrentVelocity(),
			aniId = -1,
			moveType = (byte)_moveType
		};

		using PacketWriter pw = new(false);
		pw.Write(moveToTarget);
		SendToNearbyPlayers(pw.ToPacket(), playerClients);
	}

	private bool IsPlayerWithinAggroZone(uint serverTick, ReadOnlySpan<PlayerClient> playerClients)
	{
		if (playerClients.Length == 0)
			return false;

		double closest = double.MaxValue;
		foreach (var client in playerClients)
		{
			var distance = client.GetPosition().CalculateDistance(GetMobCurrentPosition(serverTick));
			if (distance > aggro_range)
				continue;

			if (closest < distance)
				continue;

			var status = client.GetStatus();
			if (status.hp <= 0)
				continue;

			closest = distance;
			CurrentTarget = client;
		}

		if (CurrentTarget != null)
		{
			_currentState = MOB_STATE.CHASING;
			return true;
		}
		return false;
	}
}
