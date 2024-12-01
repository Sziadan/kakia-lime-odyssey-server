using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Network;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_PING)]
class CS_PING_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var ping = PacketConverter.Extract<CS_PING>(p.Payload);
		SC_PONG pong = new() { tick = LimeServer.GetCurrentTick() };
		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(pong);
		client.Send(pw.ToPacket(), default).Wait();
	}
}
