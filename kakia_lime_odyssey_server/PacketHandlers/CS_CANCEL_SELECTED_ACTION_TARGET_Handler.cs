using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.SC;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_CANCEL_SELECTED_ACTION_TARGET)]
class CS_CANCEL_SELECTED_ACTION_TARGET_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		client.SetCurrentTarget(0);
		SC_CANCEL_SELECTED_ACTION_TARGET cancel = new() {  objInstID = client.GetObjInstID() };
		using (PacketWriter pw = new(client.GetClientRevision() == 345))
		{
			pw.Write(cancel);
			client.SendGlobalPacket(pw.ToPacket(), default);
		}

		if (!client.InCombat())
			return;

		client.StopCombat();
		SC_STOP_COMBATING sc_stop_combat = new()
		{
			instID = client.GetObjInstID()
		};

		using (PacketWriter pw = new(client.GetClientRevision() == 345))
		{
			pw.Write(sc_stop_combat);
			client.Send(pw.ToPacket(), default).Wait();
			client.SendGlobalPacket(pw.ToPacket(), default).Wait();
		}
	}
}
