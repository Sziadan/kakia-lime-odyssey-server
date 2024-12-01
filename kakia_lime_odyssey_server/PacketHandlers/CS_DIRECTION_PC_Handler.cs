using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Network;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_DIRECTION_PC)]
class CS_DIRECTION_PC_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var cs_dir = PacketConverter.Extract<CS_DIRECTION_PC>(p.Payload);
		client.UpdateDirection(cs_dir.dir);

		SC_DIRECTION sc_dir = new()
		{
			objInstID = client.GetObjInstID(),
			dir = cs_dir.dir,
			tick = LimeServer.GetCurrentTick()
		};

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(sc_dir);

		client.SendGlobalPacket(pw.ToPacket(), default);
	}
}
