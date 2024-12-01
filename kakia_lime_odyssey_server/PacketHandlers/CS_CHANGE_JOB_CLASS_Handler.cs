using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.SC;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_CHANGE_JOB_CLASS)]
class CS_CHANGE_JOB_CLASS_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var cs_job = PacketConverter.Extract<CS_CHANGE_JOB_CLASS>(p.Payload);

		SC_CHANGED_JOB_CLASS sc_job = new()
		{
			instID = client.GetObjInstID(),
			jobClass = cs_job.jobClass
		};

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(sc_job);

		client.ChangeJob(cs_job.jobClass);

		client.Send(pw.ToPacket(), default).Wait();
		client.SendGlobalPacket(pw.ToPacket(), default).Wait();


		// Show the changed appearance!
		SC_UPDATED_APPEARANCE_PC sc_update_appearance = new()
		{
			objInstID = client.GetObjInstID(),
			appearance = client.GetCurrentCharacter().appearance.AsStruct()
		};

		using PacketWriter pw2 = new(client.GetClientRevision() == 345);
		pw2.Write(sc_update_appearance);

		client.Send(pw2.ToPacket(), default).Wait();
		client.SendGlobalPacket(pw2.ToPacket(), default).Wait();
	}
}
