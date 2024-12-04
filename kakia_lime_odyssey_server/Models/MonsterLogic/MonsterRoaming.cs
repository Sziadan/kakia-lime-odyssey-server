using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_server.Network;

namespace kakia_lime_odyssey_server.Models.MonsterLogic;

public partial class Monster : INPC
{
	private void Roam(uint serverTick, ReadOnlySpan<PlayerClient> playerClients)
	{
		if (IsMoving)
		{
			MoveTowardsDestination(serverTick, playerClients);
			return;
		}

		Random rnd = new Random();
		if (_actionStartTick < 20 + rnd.Next(0, 100))
		{
			_actionStartTick++;
			return;
		}

		FPOS newDestination;
		do { newDestination = _originalPosition.GetRandomPositionWithinRadius(250); } while (_destination.Compare(Position));
		SetNewDestination(newDestination, serverTick);

		// Turn monster towards destination
		var sc_stop = GetStopPacket(newDestination, serverTick);
		SendToNearbyPlayers(sc_stop, playerClients);
	}
}
