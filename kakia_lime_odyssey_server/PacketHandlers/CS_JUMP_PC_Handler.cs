using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Network;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_JUMP_PC)]
class CS_JUMP_PC_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var cs_jump = PacketConverter.Extract<CS_JUMP_PC>(p.Payload);

		client.UpdatePosition(cs_jump.pos);
		client.UpdateDirection(cs_jump.dir);

		var vel = client.GetVELOCITIES();		

		SC_JUMP_PC sc_jump = new()
		{
			objInstID = client.GetObjInstID(),
			pos = cs_jump.pos,
			dir = cs_jump.dir,
			deltaLookAtRadian = cs_jump.deltaLookAtRadian,
			tick = LimeServer.GetCurrentTick(),
			velocity = client.IsInMotion() ? vel.run : 0,
			accel = client.IsInMotion() ? vel.runAccel : 0,
			ratio = client.IsInMotion() ? vel.ratio : 0,
			isSwim = cs_jump.isSwim
		};

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(sc_jump);

		client.SendGlobalPacket(pw.ToPacket(), default);
	}
}
