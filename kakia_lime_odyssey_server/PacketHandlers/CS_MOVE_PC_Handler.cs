using kakia_lime_odyssey_logging;
using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.Enums;
using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Network;
using System.Globalization;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_MOVE_PC)]
class CS_MOVE_PC_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var move_pc = PacketConverter.Extract<CS_MOVE_PC>(p.Payload);
		var vel = client.GetVELOCITIES();

		SC_MOVE sc_move = new()
		{
			objInstID = client.GetObjInstID(),
			pos = move_pc.pos,
			dir = move_pc.dir,
			deltaLookAtRadian = move_pc.deltaLookAtRadian,
			tick = LimeServer.GetCurrentTick(),
			moveType = move_pc.moveType,
			turningSpeed = 1,
			accel = GetAcceleration((MOVE_TYPE)move_pc.moveType, vel),
			velocity = GetVelocity((MOVE_TYPE)move_pc.moveType, vel),
			velocityRatio = vel.ratio
		};
		//Logger.Log($"POS: {move_pc.pos.x} | {move_pc.pos.y} | {move_pc.pos.z}");
		client.UpdatePosition(move_pc.pos);
		client.UpdateDirection(move_pc.dir);
		client.SetInMotion(true);

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(sc_move);
		client.SendGlobalPacket(pw.ToPacket(), default).Wait();
	}

	public float GetAcceleration(MOVE_TYPE type, VELOCITIES vel)
	{
		return type switch
		{
			MOVE_TYPE.MOVE_TYPE_WALK => vel.walkAccel,
			MOVE_TYPE.MOVE_TYPE_RUN => vel.runAccel,
			MOVE_TYPE.MOVE_TYPE_SWIM => vel.swimAccel,
			_ => 1
		};
	}

	public float GetVelocity(MOVE_TYPE type, VELOCITIES vel)
	{
		return type switch
		{
			MOVE_TYPE.MOVE_TYPE_WALK => vel.walk,
			MOVE_TYPE.MOVE_TYPE_RUN => vel.run,
			MOVE_TYPE.MOVE_TYPE_SWIM => vel.swim,
			_ => 1
		};
	}
}
