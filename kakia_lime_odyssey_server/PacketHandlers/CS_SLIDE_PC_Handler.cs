using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.Enums;
using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Network;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_SLIDE_PC)]
class CS_SLIDE_PC_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var cs_slide = PacketConverter.Extract<CS_SLIDE_PC>(p.Payload);

		client.UpdatePosition(cs_slide.pos);
		client.UpdateDirection(cs_slide.dir);
		client.SetInMotion(true);

		var vel = client.GetVELOCITIES();

		SC_SLIDE_PC sc_slide = new()
		{
			objInstID = client.GetObjInstID(),
			pos = cs_slide.pos,
			dir = cs_slide.dir,
			deltaLookAtRadian = cs_slide.deltaLookAtRadian,
			deltaBodyRadian = cs_slide.deltaBodyRadian,
			velDecRatio = cs_slide.velDecRatio,
			tick = LimeServer.GetCurrentTick(),
			moveType = cs_slide.moveType,
			turningSpeed = cs_slide.turningSpeed,
			accel = GetAcceleration((MOVE_TYPE)cs_slide.moveType, vel),
			velocity = GetVelocity((MOVE_TYPE)cs_slide.moveType, vel),
			velocityRatio = vel.ratio
		};

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(sc_slide);

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
