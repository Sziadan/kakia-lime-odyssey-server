using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.SC;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_REQUEST_COMMON_STATUS)]
class CS_REQUEST_COMMON_STATUS_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var req = PacketConverter.Extract<CS_REQUEST_COMMON_STATUS>(p.Payload);
		var status = client.RequestCommonStatus(req.objInstID);

		SC_COMMON_STATUS sc_status = new() 
		{ 
			objInstID = req.objInstID,
			status = status 
		};

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(sc_status);

		client.Send(pw.ToPacket(), default).Wait();
	}
}
