using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Network;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_STOP_PC)]
class CS_STOP_PC_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var cs_stop = PacketConverter.Extract<CS_STOP_PC>(p.Payload);

		client.UpdatePosition(cs_stop.pos);
		client.UpdateDirection(cs_stop.dir);
		client.SetInMotion(false);

		SC_STOP sc_stop = new()
		{
			objInstID = client.GetObjInstID(),
			pos = cs_stop.pos,
			dir = cs_stop.dir,
			tick = LimeServer.GetCurrentTick(),
			stopType = cs_stop.stopType
		};

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(sc_stop);

		client.SendGlobalPacket(pw.ToPacket(), default).Wait();
	}
}
