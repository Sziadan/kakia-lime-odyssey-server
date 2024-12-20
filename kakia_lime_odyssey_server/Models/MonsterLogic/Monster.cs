﻿using kakia_lime_odyssey_logging;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.Enums;
using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Database;
using kakia_lime_odyssey_server.Interfaces;
using kakia_lime_odyssey_server.Models.MonsterXML;
using kakia_lime_odyssey_server.Network;

namespace kakia_lime_odyssey_server.Models.MonsterLogic;

public partial class Monster : INPC, IEntity
{
	private readonly XmlMonster _mobInfo;

	public FPOS Position { get; set; }
	public FPOS Direction { get; set; }
	public uint Zone { get; set; }

	public uint Id;
	public uint ModelId;
	public string Name;

	private uint Lv { get; set; }
	public uint MHP { get; set; }
	public uint HP { get; set; }
	public uint MMP { get; set; }
	public uint MP { get; set; }

	private MOB_STATE _currentState { get; set; }

	public PlayerClient? CurrentTarget { get; set; }
	public bool Aggro { get; set; }

	private FPOS _destination;
	private FPOS _originalPosition;

	private uint _actionStartTick;
	private uint _lastTick;
	public bool IsMoving;

	private MOVE_TYPE _moveType = MOVE_TYPE.MOVE_TYPE_WALK;

	private readonly uint update_distance = 1500;
	private readonly uint aggro_range = 150;

	private List<LootableItem> Lootable { get; set; }
	private List<Item> Loot { get; set; }

	public Monster(XmlMonster mobInfo, uint id, FPOS pos, FPOS dir, uint zone, bool aggro, int lootTable)
	{
		_mobInfo = mobInfo;
		Position = pos;
		Direction = dir;
		Zone = zone;
		Id = id;
		_originalPosition = pos;

		Lv = (uint)_mobInfo.Level;
		MMP = (uint)_mobInfo.MMP;
		MHP = (uint)_mobInfo.MHP;
		MP = (uint)_mobInfo.MP;
		HP = (uint)_mobInfo.HP;
		Aggro = aggro;

		Name = mobInfo.Name;
		ModelId = (uint)_mobInfo.ModelTypeID;

		CurrentTarget = null;
		_currentState = MOB_STATE.ROAMING;

		Lootable = new List<LootableItem>();
		var dropTable = JsonDB.LoadItemDropTable();
		if (dropTable == null)
			return;
		

		if (dropTable.ContainsKey(lootTable))
		{
			Lootable.AddRange(dropTable[lootTable]);
		}
	}

	public SC_ENTER_SIGHT_MONSTER GetEnterSight()
	{
		return GetMob().GetEnterSight();
	}

	public MOB GetMob()
	{
		return new MOB()
		{
			Id = (int)Id,
			Pos = Position,
			Dir = Direction,
			Appearance = new()
			{
				appearance = new()
				{
					name = Name,
					action = _mobInfo.Subjects.First().EventID,
					actionStartTick = 0,
					scale = 1,
					transparent = 1,
					modelTypeID = ModelId,
					color = new()
					{
						r = 0,
						g = 0,
						b = 0
					},
					typeID = (uint)_mobInfo.TypeID
				},
				aggresive = Aggro,
				shineWhenHitted = _mobInfo.ShineWhenHitted != 0
			},
			RaceRelationState = 0,
			StopType = 0,
			ZoneId = Zone,
			Status = new()
			{
				lv = (byte)Lv,
				mhp = MHP,
				hp = HP,
				mmp = MMP,
				mp = MP
			}
		};
	}

	public void Update(uint serverTick, ReadOnlySpan<PlayerClient> playerClients)
	{
		if (serverTick - _lastTick < 100)
			return;
		_lastTick = serverTick;

		// Monster dead
		if (HP == 0)
		{
			if (CurrentTarget != null)
				CurrentTarget = null;

			if (serverTick - _actionStartTick > 60_000)
			{
				// Despawn
			}

			return;
		}


		if (CurrentTarget != null)
		{
			bool updated = false;
			foreach(PlayerClient client in playerClients)
			{
				if (client.GetObjInstID() == CurrentTarget.GetObjInstID())
				{
					CurrentTarget = client;
					updated = true;
					break;
				}
			}

			if (!updated)
				CurrentTarget = null;
		}


		switch(_currentState)
		{
			case MOB_STATE.ROAMING:
				if (IsPlayerWithinAggroZone(serverTick, playerClients))
					return;

				Roam(serverTick, playerClients);
				break;


			case MOB_STATE.CHASING:
				ChasePlayer(serverTick, playerClients);
				break;


			case MOB_STATE.ATTACKING:
				AttackPlayer(serverTick, playerClients);
				break;
		}
	}

	private void MoveTowardsDestination(uint serverTick, ReadOnlySpan<PlayerClient> playerClients, double distance = 0)
	{
		var currentPosition = GetMobCurrentPosition(serverTick);
		if (currentPosition.IsNaN())
			currentPosition = Position;

		double distanceToTarget = currentPosition.CalculateDistance(_destination);
		var dir = currentPosition.CalculateDirection(_destination);

		if (!dir.IsNaN())
		{
			Direction = dir;
			SC_DIRECTION sc_dir = new()
			{
				objInstID = Id,
				dir = Direction,
				tick = LimeServer.GetCurrentTick()
			};
			using PacketWriter pw = new(false);
			pw.Write(sc_dir);
			SendToNearbyPlayers(pw.ToPacket(), playerClients);
		}

		

		if (currentPosition.Compare(_destination) || distanceToTarget <= distance)
		{
			Position = currentPosition;
			_destination = default;


			IsMoving = false;
			_actionStartTick = 0;

			var sc_stop = GetStopPacket(Position, serverTick);
			SendToNearbyPlayers(sc_stop, playerClients);
			return;
		}

		var pck = GetMovePacket(currentPosition, serverTick);
		SendToNearbyPlayers(pck, playerClients);
	}

	private void SendToNearbyPlayers(byte[] packet, ReadOnlySpan<PlayerClient> playerClients)
	{
		foreach (var client in playerClients)
		{
			if (!client.IsLoaded() || client.GetPosition().CalculateDistance(Position) > update_distance)
				continue;

			client.Send(packet, default).Wait();
		}
	}

	private FPOS GetMobCurrentPosition(uint serverTick)
	{
		if (!IsMoving)
			return Position;

		var currentPos = Position.CalculateCurrentPosition(_destination, GetCurrentVelocity(), (serverTick - _actionStartTick) / 1000);
		if (currentPos.IsNaN())
			return _destination;

		return currentPos;
	}

	private void ReturnHome(uint serverTick)
	{
		CurrentTarget = null;
		_currentState = MOB_STATE.ROAMING;
		_moveType = MOVE_TYPE.MOVE_TYPE_WALK;
		Position = GetMobCurrentPosition(serverTick);
		IsMoving = false;
		SetNewDestination(_originalPosition, serverTick);
	}

	private void SetNewDestination(FPOS destination, uint serverTick)
	{
		_destination = destination;

		var dir = Position.CalculateDirection(_destination);
		if (!dir.IsNaN())
			Direction = dir;

		if (!IsMoving)
			_actionStartTick = serverTick;

		IsMoving = true;		
	}

	private byte[] GetMovePacket(FPOS currentPosition, uint serverTick)
	{
		var vel = GetVelocities();
		SC_MOVE sc_move = new()
		{
			objInstID = Id,
			pos = currentPosition,
			dir = Direction,
			deltaLookAtRadian = 0,
			tick = serverTick,
			moveType = (byte)_moveType,
			turningSpeed = 1,
			accel = 0,
			velocity = GetCurrentVelocity(),
			velocityRatio = 1
		};

		using PacketWriter pw = new(false);
		pw.Write(sc_move);
		return pw.ToPacket();
	}

	private byte[] GetStopPacket(FPOS position, uint serverTick)
	{
		SC_STOP sc_stop = new()
		{
			objInstID = Id,
			pos = position,
			dir = Direction,
			tick = serverTick,
			stopType = 0
		};

		using PacketWriter pw = new(false);
		pw.Write(sc_stop);

		return pw.ToPacket();
	}

	private float GetCurrentVelocity()
	{
		var velocities = GetVelocities();
		return _moveType switch
		{
			MOVE_TYPE.MOVE_TYPE_RUN => velocities.run,
			MOVE_TYPE.MOVE_TYPE_WALK => velocities.walk,
			_ => velocities.walk
		};
	}

	private VELOCITIES GetVelocities()
	{
		return new VELOCITIES()
		{
			ratio = 1,
			run = (float)_mobInfo.RunVelocity,
			runAccel = (float)_mobInfo.RunAccel,
			walk = (float)_mobInfo.WalkVelocity,
			walkAccel = (float)_mobInfo.WalkAccel,
			backwalk = (float)_mobInfo.BackwalkVelocity,
			backwalkAccel = (float)_mobInfo.BackwalkAccel,
			swim = (float)_mobInfo.FastSwimVelocity,
			swimAccel = (float)_mobInfo.FastSwimAccel,
			backSwim = (float)_mobInfo.BackSwimVelocity,
			backSwimAccel = (float)_mobInfo.BackSwimAccel
		};
	}

	public NPC_TYPE GetNPCType()
	{
		return NPC_TYPE.MOB;
	}

	public long GetId()
	{
		return Id;
	}

	public FPOS GetPosition()
	{
		return GetMobCurrentPosition(LimeServer.GetCurrentTick());
	}

	public FPOS GetDirection()
	{
		return Direction;
	}

	public EntityStatus GetEntityStatus()
	{
		return new EntityStatus()
		{
			Lv = (byte)this.Lv,
			BaseStats = new(),
			BasicStatus = new()
			{
				Hp = this.HP,
				MaxHp = this.MHP,
				Mp = this.MP,
				MaxMp = this.MMP
			},
			MeleeAttack = new()
			{
				WeaponTypeId = (uint)_mobInfo.Model.WeaponType,
				Atk = (ushort)_mobInfo.BaseMeleeAtk,
				Def = (ushort)_mobInfo.BaseMeleeDefense,
				Hit = (ushort)_mobInfo.BaseMeleeHitRate,
				CritRate = (ushort)_mobInfo.BaseCriticalRate,
				FleeRate = (ushort)_mobInfo.BaseDodge
			},
			SpellAttack = new()
			{
				Atk = (ushort)_mobInfo.BaseSpellAtk,
				Def = (ushort)_mobInfo.BaseSpellDefense,
				Hit = (ushort)_mobInfo.BaseSpellHitRate,
				CritRate = (ushort)_mobInfo.BaseCriticalRate,
				FleeRate = (ushort)_mobInfo.BaseDodge
			}
		};
	}

	public DamageResult UpdateHealth(int healthChange)
	{
		var newHealth = (HP + healthChange);
		HP = (uint)(newHealth <= 0 ? 0 : newHealth);

		if (HP == 0)
			_actionStartTick = LimeServer.GetCurrentTick();

		return new DamageResult()
		{
			TargetKilled = HP == 0,
			ExpReward = _mobInfo.CombatJobEXP + _mobInfo.EXP
		};
	}

	public bool AddExp(ulong exp)
	{
		throw new NotImplementedException();
	}

	public List<Item> GetLoot()
	{
		if (Loot == null)
		{
			if (Lootable.Count == 0)
				return new List<Item>();

			Loot = new List<Item>();
			Random rnd = new Random();
			foreach(var lootItem in Lootable)
			{
				var odds = rnd.Next(0, 10000);
				if (lootItem.DropRate * 100 < odds)
					continue;

				Loot.Add(LimeServer.ItemDB.First(item => item.Id == lootItem.Id));
			}
		}

		return Loot;
	}

	void IEntity.Loot(Item item)
	{
		if (Loot.Contains(item))
			Loot.Remove(item);
	}
}
