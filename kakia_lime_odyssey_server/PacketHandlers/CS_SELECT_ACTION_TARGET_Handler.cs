using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.SC;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_SELECT_ACTION_TARGET)]
class CS_SELECT_ACTION_TARGET_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var cs_target = PacketConverter.Extract<CS_SELECT_ACTION_TARGET>(p.Payload);
		SC_SELECT_ACTION_TARGET sc_target = new()
		{
			objInstID = client.GetObjInstID(),
			targetInstID = cs_target.targetInstID
		};

		client.SetCurrentTarget(cs_target.targetInstID);

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(sc_target);
		client.Send(pw.ToPacket(), default).Wait();
		client.SendGlobalPacket(pw.ToPacket(), default).Wait();
	}
}
