using kakia_lime_odyssey_logging;
using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Database;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_CREATE_PC)]
class CS_CREATE_PC_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		Logger.LogPck(p.Payload);
		var character = PacketConverter.Extract<CS_CREATE_PC_KR>(p.Payload);

		CLIENT_PC newCharacter = new()
		{
			status = new SAVED_STATUS_PC_KR()
			{
			
				hp = 120,
				mp = 120,
				lp = 120,
				streamPoint = 1,
				breath = 1,
				fatigue = 1,
				
				lifeJob = new SAVED_LIFE_JOB_STATUS()
				{
					lv = 1,
					exp = 1,
					statusPoint = 0,
					idea = 0,
					mind = 0,
					craft = 0,
					sense = 0
				},

				combatJob = new SAVED_COMBAT_JOB_STATUS()
				{
					lv = 1,
					exp = 1,
					strength = 1,
					intelligence = 1,
					dexterity = 1,
					agility = 1,
					vitality = 1,
					spirit = 1,
					lucky = 1
				}
			},

			appearance = new()
			{
				name = character.name,
				raceTypeID = character.raceTypeID,
				genderType = character.genderType,
				lifeJobTypeID = character.lifeJobTypeID,
				combatJobTypeID = 1,
				headType = character.headType,
				hairType = character.hairType,
				eyeType = character.eyeType,
				earType = character.earType,
				underwearType = character.underwearType,
				familyNameType = 0,
				skinColorType = character.skinColorType,
				hairColorType = character.hairColorType,
				eyeColorType = character.eyeColorType,
				eyeBrowColorType = character.eyeBrowColorType,
				action = 0,
				actionStartTick = 0,
				color = new()
				{
					r = 1,
					g = 1,
					b = 1
				},
				playingJobClass = 0,
				scale = 1,
				showHelm = true,
				transparent = 1,
				equiped = new()
				{
					NONE = 0,
					MAIN_EQUIP = 0,
					SUB_EQUIP = 0,
					RANGE_MAIN_EQUIP = 0,
					SPENDING = 0,
					HEAD = 0,
					FOREHEAD = 0,
					EYE = 0,
					MOUTH = 0,
					NECK = 0,
					SHOULDER = 0,
					UPPER_BODY = 0,
					HAND = 0,
					WAIST = 0,
					LOWER_BODY = 0,
					FOOT = 0,
					RELIC = 0,
					RING_1 = 0,
					RING_2 = 0,
					ACCESSORY_1 = 0
				}
			}
		};

		JsonDB.StoreAppearance(client.GetAccountId(), character.name, newCharacter.appearance);
		JsonDB.StoreSavedStatusPC(client.GetAccountId(), character.name, newCharacter.status);

		SC_CREATED_PC sc_created_pc = new()
		{
			client_pc = newCharacter
		};

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(sc_created_pc);

		client.Send(pw.ToSizedPacket(), default);
	}
}
