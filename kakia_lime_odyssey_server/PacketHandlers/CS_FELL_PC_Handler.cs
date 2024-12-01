using kakia_lime_odyssey_logging;
using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_FELL_PC)]
class CS_FELL_PC_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		// TODO: Do something here...

		//Logger.Log("[CS_FELL_PC]");
		//Logger.LogPck(p.Payload);
	}
}
