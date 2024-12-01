using kakia_lime_odyssey_logging;
using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Database;
using kakia_lime_odyssey_server.Models;
using kakia_lime_odyssey_server.Network;
using kakia_lime_odyssey_utils.Extensions;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_START_GAME)]
class CS_START_GAME_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var selectedCharacter = PacketConverter.Extract<CS_START_GAME>(p.Payload);
		var characters = JsonDB.LoadPC(client.GetAccountId());
		var current = characters[selectedCharacter.charNum];
		client.SetCurrentCharacter(current);

		if (client.GetPosition().Equals(default(FPOS)))
		{
			client.SetZone(2);
			client.UpdatePosition(new()
			{
				x = -478.353821F,
				y = 361.71F,
				z = 833.2319F
			});
			client.UpdateDirection(new()
			{
				x = 0.8577117F,
				y = 0.514131069F,
				z = 0.0F
			});
		}

		SC_TICK tick = new()
		{
			tick = LimeServer.GetCurrentTick()
		};

		using (PacketWriter pw = new(client.GetClientRevision() == 345))
		{
			pw.Write(tick);
			client.Send(pw.ToPacket(), default).Wait();
		}

		SC_READY ready = new()
		{
			zoneTypeID = client.GetZone(),
			pc = client.GetRegionPC()
		};

		using (PacketWriter pw = new(client.GetClientRevision() == 345))
		{
			pw.Write(ready);
			client.Send(pw.ToPacket(), default).Wait();
		}

		
		SC_ENTER_ZONE sc_enter_zone = new()
		{
			objInstID = client.GetObjInstID()
		};

		using (PacketWriter pw = new(client.GetClientRevision() == 345))
		{
			pw.Write(sc_enter_zone);
			client.Send(pw.ToPacket(), default).Wait();
		}
		

		// FIX THESE
		client.SendInventory();
		client.SendEquipment();
	}
}
