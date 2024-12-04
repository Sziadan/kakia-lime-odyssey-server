using kakia_lime_odyssey_logging;
using kakia_lime_odyssey_packets.Packets.Enums;
using kakia_lime_odyssey_packets.Packets.Models;
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

			Logger.Log($"Current Distance: {distanceToPlayer}");

			if (distanceToPlayer > aggro_range || distanceFromHome > 250)
			{
				ReturnHome(serverTick);
				return;
			}

			_moveType = MOVE_TYPE.MOVE_TYPE_RUN;

			var closeToPlayer = CurrentTarget.GetPosition();
			if (!closeToPlayer.IsNaN())
			{
				SetNewDestination(closeToPlayer, serverTick);
				MoveTowardsDestination(serverTick, playerClients, 5);
			}

			if (distanceToPlayer > 0)
			{
				var dir = currentPosition.CalculateDirection(CurrentTarget.GetPosition());
				if (!dir.IsNaN())
				{
					Direction = dir;
				}
			}
		}
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
