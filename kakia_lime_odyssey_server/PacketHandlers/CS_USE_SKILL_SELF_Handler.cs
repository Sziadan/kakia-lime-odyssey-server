using kakia_lime_odyssey_logging;
using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Network;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_USE_SKILL_SELF)]
class CS_USE_SKILL_SELF_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var useSkill = PacketConverter.Extract<CS_USE_SKILL_ACTION_TARGET>(p.Payload);

		var skill = LimeServer.SkillDB.FirstOrDefault(skill => skill.Id == useSkill.typeID);
		if (skill is null)
		{
			Logger.Log($"Skill not found with ID: {useSkill.typeID}!", LogLevel.Error);
			return;
		}

		// Go with skill level 1 for now, since we haven't actually implemented skills for real
		var skillLv1 = skill.Subject.SubjectLists.FirstOrDefault();
		uint castTime = (uint)(skillLv1 != null ? skillLv1.CastingTime : skill.CastingTime);

		if (castTime > 0)
		{
			SC_START_CASTING_SKILL_OBJ castSkill = new()
			{
				fromInstID = client.GetObjInstID(),
				targetInstID = client.GetObjInstID(),
				typeID = useSkill.typeID,
				castTime = castTime
			};

			using (PacketWriter pw = new(client.GetClientRevision() == 345))
			{
				pw.Write(castSkill);
				client.Send(pw.ToPacket(), default).Wait();
				client.SendGlobalPacket(pw.ToPacket(), default).Wait();
			}
		}

		SC_USE_SKILL_OBJ_RESULT_LIST actionSkill = new()
		{
			fromInstID = client.GetObjInstID(),
			toInstID = client.GetObjInstID(),
			typeID = useSkill.typeID,
			useHP = (short)(skillLv1  != null ? skillLv1.UseHP : 0),
			useMP = (short)(skillLv1 != null ? skillLv1.UseMP : 0),
			useLP = (short)(skillLv1 != null ? skillLv1.UseLP : 0),
			useSP = (short)(skillLv1 != null ? skillLv1.UseSP : 0),
			coolTime = (uint)(skillLv1 != null ? skillLv1.CoolTime : skill.CoolTime)
		};

		using (PacketWriter pw = new(client.GetClientRevision() == 345))
		{
			pw.Write(actionSkill);
			client.Send(pw.ToSizedPacket(), default).Wait();
			client.SendGlobalPacket(pw.ToSizedPacket(), default).Wait();
		}
	}
}
