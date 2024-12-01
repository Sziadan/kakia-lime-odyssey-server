using kakia_lime_odyssey_logging;
using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.SC;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_READY)]
class CS_READY_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		if (client.IsLoaded()) return;

		client.SetLoaded();
		client.RequestPresence(default).Wait();
	}
}
