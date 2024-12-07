using kakia_lime_odyssey_logging;
using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Database;
using kakia_lime_odyssey_server.Models;
using kakia_lime_odyssey_server.Models.MonsterLogic;
using kakia_lime_odyssey_server.Network;
using System;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_SAY_PC)]
class CS_SAY_PC_Handler : PacketHandler
{
	private bool IsCommand(CS_SAY_PC say)
	{
		return say.message.StartsWith("#");
	}

	private void HandleCommand(CS_SAY_PC say, IPlayerClient client)
	{
		string[] parts = say.message.Split(' ');
		switch (parts[0])
		{
			case "#update":
				Update(parts, client);
				break;

			case "#spawn":
				Spawn(parts, client);
				break;

			case "#warp":
				Warp(parts, client);
				break;

			case "#item":
				GetItem(parts, client);
				break;

			case "#doaction":
				PerformAction(parts, client);
				break;

			case "#stopaction":
				StopAction(parts, client);
				break;
		}
	}

	private void StopAction(string[] parts, IPlayerClient client)
	{
		SC_FINISH_CONTINUOUS_ACTION action = new()
		{
			instID = client.GetObjInstID()
		};

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(action);

		client.Send(pw.ToPacket(), default).Wait();
		client.SendGlobalPacket(pw.ToPacket(), default).Wait();
	}

	private void PerformAction(string[] parts, IPlayerClient client)
	{
		uint id = uint.Parse(parts[1]);

		SC_START_CONTINUOUS_ACTION action = new()
		{
			instID = client.GetObjInstID(),
			action = id
		};

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(action);

		client.Send(pw.ToPacket(), default).Wait();
		client.SendGlobalPacket(pw.ToPacket(), default).Wait();
		Logger.Log($"Sent action {id}");
	}

	private void GetItem(string[] parts, IPlayerClient client)
	{
		int id = int.Parse(parts[1]);
		var inventory = client.GetInventory();

		var item = LimeServer.ItemDB.FirstOrDefault(m => m.Id == id);
		if (item is null)
			return;

		var slot = inventory.AddItem(item);

		SC_INSERT_SLOT_ITEM sc_item = new()
		{
			slot = new()
			{
				type = (byte)0, // 0 == inventory | 1 == bank
				slot = slot
			},
			typeID = item.Id,
			count = 1,
			durability = 200,
			mdurability = 200,
			remainExpiryTime = 0,
			grade = item.Grade,
			inherits = item.GetInherits()
		};

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(sc_item);
		client.Send(pw.ToPacket(), default).Wait();
	}

	private void Warp(string[] parts, IPlayerClient client)
	{
		if (parts.Length < 4) 
		{
			Logger.Log("Warp command, missing arguements.", LogLevel.Error);
			return;
		}

		FPOS pos = new()
		{
			x = float.Parse(parts[1]),
			y = float.Parse(parts[2]),
			z = float.Parse(parts[3])
		};

		client.UpdatePosition(pos);
		SC_WARP warp = new()
		{
			objInstID = client.GetObjInstID(),
			pos = pos,
			dir = client.GetDirection()
		};

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(warp);
		client.Send(pw.ToPacket(), default).Wait();
		client.SendGlobalPacket(pw.ToPacket(), default).Wait();		
	}

	private void Spawn(string[] parts, IPlayerClient client)
	{
		switch (parts[1])
		{
			case "npc":
				SpawnNpc(client, uint.Parse(parts[2]));
				break;

			case "mob":
				SpawnMob(client, uint.Parse(parts[2]), bool.Parse(parts[3]));
				break;
		}
	}

	private void SpawnNpc(IPlayerClient client, uint id)
	{
		int objId = Random.Shared.Next(210000, 250000);

		NPC npc = new()
		{
			Id = objId,
			Pos = client.GetPosition(),
			Dir = client.GetDirection(),
			Appearance = new()
			{
				appearance = new()
				{
					name = "Test Villager",
					action = 0,
					actionStartTick = 4,
					scale = 1,
					transparent = 1,
					modelTypeID = id,
					color = new()
					{
						r = 0,
						g = 0,
						b = 0
					},
					typeID = 0
				},
				specialistType = 0
			},
			RaceRelationState = 0,
			StopType = 0,
			ZoneId = client.GetZone(),
			Status = new COMMON_STATUS()
			{
				lv = 1,
				mhp = 200,
				hp = 200,
				mmp = 200,
				mp = 200
			}
		};

		client.AddNpcOrMob(npc);

		/*
		using PacketWriter pw = new();
		pw.Write(villager);
		client.Send(pw.ToSizedPacket(), default).Wait();
		*/
	}

	private void SpawnMob(IPlayerClient client, uint id, bool aggro) 
	{
		int objId = Random.Shared.Next(210000, 250000);

		var monster = LimeServer.MonsterDB.FirstOrDefault(m => m.ModelTypeID == id);
		if (monster == null)
		{
			Logger.Log("No monster found");
			return;
		}

		Monster mob = new Monster(monster, (uint)objId, client.GetPosition(), client.GetDirection(), client.GetZone(), false, 0);
		client.AddNpcOrMob(mob);
	}

	private void Update(string[] parts, IPlayerClient client)
	{
		switch(parts[1])
		{
			case "velocity":
				UpdateVelocity(parts, client);
				break;
		}
	}

	private void UpdateVelocity(string[] parts, IPlayerClient client)
	{
		var vel = client.GetVELOCITIES();
		float val = float.Parse(parts[3]);

		switch (parts[2])
		{
			case "ratio":
				vel.ratio = val;				
				break;

			case "run":
				vel.run = val;
				break;

			case "runAccel":
				vel.runAccel = val;
				break;

			case "walk":
				vel.walk = val;
				break;

			case "walkAccel":
				vel.walkAccel = val;
				break;

			case "backwalk":
				vel.backwalk = val;
				break;

			case "backwalkAccel":
				vel.backwalkAccel = val;
				break;

			case "swim":
				vel.swim = val;
				break;

			case "swimAccel":
				vel.swimAccel = val;
				break;

			case "backSwim":
				vel.backSwim = val;
				break;

			case "backSwimAccel":
				vel.backSwimAccel = val;
				break;

			default:
				Logger.Log($"Faulty param {parts[2]}", LogLevel.Debug);
				return;
		}

		Logger.Log($"Updated velocity param {parts[2]} to {parts[3]}", LogLevel.Debug);
		client.UpdateVELOCITIES(vel);

		SC_CHANGED_VELOCITIES updateVel = new()
		{
			velocities = vel
		};

		using (PacketWriter pw = new(client.GetClientRevision() == 345))
		{
			pw.Write(updateVel);
			client.Send(pw.ToPacket(), default).Wait();
		}
	}

	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		using PacketReader pr = new PacketReader(p.Payload);
		var cs_say = pr.Read_CS_SAY_PC(p.Size);

		Logger.Log($"Message from [{client.GetCurrentCharacter().appearance.name}] : {cs_say.message}", LogLevel.Debug);

		if (IsCommand(cs_say))
		{
			HandleCommand(cs_say, client);
			return;
		}


		SC_SAY sc_say = new()
		{
			objInstID = client.GetObjInstID(),
			maintainTime = cs_say.maintainTime,
			type = cs_say.type,
			message = cs_say.message
		};

		using PacketWriter pw = new(client.GetClientRevision() == 345);
		pw.Write(sc_say);

		client.SendGlobalPacket(pw.ToSizedPacket(), default).Wait();
	}
}
