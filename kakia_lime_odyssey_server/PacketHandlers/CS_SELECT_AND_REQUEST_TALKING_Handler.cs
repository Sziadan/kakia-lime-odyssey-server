using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.SC;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_SELECT_AND_REQUEST_TALKING)]
class CS_SELECT_AND_REQUEST_TALKING_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var req_talking = PacketConverter.Extract<CS_SELECT_AND_REQUEST_TALKING>(p.Payload);

		SC_TALKING talking = new()
		{
			objInstID = client.GetObjInstID(),
			dialog = "I got nothing to say yet."
		};

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(talking);

		client.Send(pw.ToSizedPacket(), default).Wait();
	}
}
