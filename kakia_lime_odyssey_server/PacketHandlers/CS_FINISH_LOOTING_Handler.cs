using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.SC;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_FINISH_LOOTING)]
class CS_FINISH_LOOTING_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		SC_FINISH_LOOTING sc_finish_looting = new()
		{
			objInstID = client.GetObjInstID()
		};

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(sc_finish_looting);

		client.Send(pw.ToPacket(), default).Wait();		
	}
}
