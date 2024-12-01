using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Network;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_SIT_DOWN_PC)]
class CS_SIT_DOWN_PC_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var cs_sit = PacketConverter.Extract<CS_SIT_DOWN_PC>(p.Payload);
		SC_SIT_DOWN sc_sit = new()
		{
			objInstID = client.GetObjInstID(),
			tick = LimeServer.GetCurrentTick()
		};

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(sc_sit);

		client.SendGlobalPacket(pw.ToPacket(), default);
	}
}
