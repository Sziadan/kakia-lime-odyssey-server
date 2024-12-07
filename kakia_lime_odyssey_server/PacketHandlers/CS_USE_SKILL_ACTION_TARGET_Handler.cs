using kakia_lime_odyssey_logging;
using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.Enums;
using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Interfaces;
using kakia_lime_odyssey_server.Models;
using kakia_lime_odyssey_server.Network;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_USE_SKILL_ACTION_TARGET)]
class CS_USE_SKILL_ACTION_TARGET_Handler : PacketHandler
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
		if (!LimeServer.TryGetEntity(client.GetCurrentTarget(), out IEntity? target)) return;
		if (target is null) return;

		if (target.GetEntityStatus().BasicStatus.Hp == 0)
		{
			// Target dead
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
				targetInstID = client.GetCurrentTarget(),
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
			toInstID = client.GetCurrentTarget(),
			typeID = useSkill.typeID,
			useHP = (short)(skillLv1 != null ? skillLv1.UseHP : 0),
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

		var damage = DamageHandler.DealWeaponHitDamage((client as IEntity)!, target);
		if (damage.Packet is null) return;

		client.Send(damage.Packet, default).Wait();
		client.SendGlobalPacket(damage.Packet, default).Wait();

		var result = target.UpdateHealth((int)(damage.Damage * -1));
		if (result.TargetKilled)
		{
			SC_STOP sc_stop = new()
			{
				objInstID = target.GetId(),
				pos = target.GetPosition(),
				dir = target.GetDirection(),
				tick = LimeServer.GetCurrentTick(),
				stopType = 0
			};

			using PacketWriter pw_stop = new(false);
			pw_stop.Write(sc_stop);
			client.Send(pw_stop.ToPacket(), default).Wait();
			client.SendGlobalPacket(pw_stop.ToPacket(), default).Wait();


			using (PacketWriter pw = new(false))
			{
				SC_DEAD dead = new()
				{
					objInstID = target.GetId()
				};

				pw.Write(dead);
				client.Send(pw.ToPacket(), default).Wait();
				client.SendGlobalPacket(pw.ToPacket(), default).Wait();
			}

			if (result.ExpReward == 0)
				return;

			var pcEntity = client as IEntity;
			var levelUp = pcEntity.AddExp((ulong)result.ExpReward);
			var currentStatus = pcEntity.GetEntityStatus();

			using (PacketWriter pw = new(false))
			{
				SC_GOT_COMBAT_JOB_EXP addExp = new()
				{
					exp = (uint)currentStatus.Exp,
					addExp = (uint)result.ExpReward
				};
				pw.Write(addExp);
				client.Send(pw.ToPacket(), default).Wait();
			}

			if (!levelUp)
				return;



			using (PacketWriter pw = new(false))
			{
				SC_PC_COMBAT_JOB_LEVEL_UP lvUp = new()
				{
					objInstID = pcEntity.GetId(),
					lv = currentStatus.Lv,
					exp = (uint)currentStatus.Exp,
					newStr = 5,
					newInl = 5,
					newAgi = 5,
					newDex = 5,
					newSpi = 5,
					newVit = 5				
				};
				pw.Write(lvUp);
				client.Send(pw.ToPacket(), default).Wait();
				client.SendGlobalPacket(pw.ToPacket(), default).Wait();
			}

		}
		
	}
}
