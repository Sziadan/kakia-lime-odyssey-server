using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.SC;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_SELECT_TARGET_READY_WEAPON_HITING)]
class CS_SELECT_TARGET_READY_WEAPON_HITING_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var target = PacketConverter.Extract<CS_SELECT_TARGET_READY_WEAPON_HITING>(p.Payload);
		client.SetCurrentTarget(target.targetInstID);

		client.InitCombat();
		SC_START_COMBATING sc_start_combat = new()
		{
			instID = client.GetObjInstID()
		};

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(sc_start_combat);
		client.Send(pw.ToPacket(), default).Wait();
		client.SendGlobalPacket(pw.ToPacket(), default).Wait();
	}
}
