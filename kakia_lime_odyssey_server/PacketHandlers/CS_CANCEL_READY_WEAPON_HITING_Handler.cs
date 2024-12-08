using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.SC;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_CANCEL_READY_WEAPON_HITING)]
class CS_CANCEL_READY_WEAPON_HITING_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		SC_STOP_COMBATING stop_combat = new()
		{
			instID = client.GetObjInstID()
		};

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(stop_combat);

		client.Send(pw.ToPacket(), default).Wait();
		client.SendGlobalPacket(pw.ToPacket(), default).Wait();
	}
}
