using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.Enums;
using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Models.MonsterXML;
using kakia_lime_odyssey_server.Network;
using System.Collections.Concurrent;

namespace kakia_lime_odyssey_server.Models;

public class Monster : INPC
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
	
	public uint CurrentTarget { get; set; }
	public bool Aggro { get; set; }

	private FPOS _destination;
	private FPOS _originalPosition;

	private uint _actionStartTick;
	private uint _lastTick;
	public bool IsMoving;

	public Monster(XmlMonster mobInfo, uint id, FPOS pos, FPOS dir, uint zone, bool aggro)
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
		HP = (uint)_mobInfo.MP;
		Aggro = aggro;

		Name = mobInfo.Name;
		ModelId = (uint)_mobInfo.ModelTypeID;

		CurrentTarget = 0;
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
					actionStartTick = 4,
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
				shineWhenHitted = true
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


		if (!IsMoving)
		{
			if(!Roam(serverTick))
				return;
		}

		var currentPosition = Position.CalculateCurrentPosition(_destination, GetVelocities().walk, (double)((serverTick - _actionStartTick)/100));

		if (currentPosition.Compare(_destination))
		{
			Position = currentPosition;
			_destination = default;


			IsMoving = false;
			_actionStartTick = 0;

			SC_STOP sc_stop = new()
			{
				objInstID = Id,
				pos = Position,
				dir = Direction,
				tick = serverTick,
				stopType = 0
			};

			using PacketWriter pw1 = new(false);
			pw1.Write(sc_stop);

			foreach (var client in playerClients)
			{
				if (!client.IsLoaded() || client.GetPosition().CalculateDistance(Position) > 500)
					continue;

				client.Send(pw1.ToPacket(), default).Wait();
			}
			return;
		}

		var pck = GetMovePacket(currentPosition, serverTick, MOVE_TYPE.MOVE_TYPE_WALK);
		foreach (var client in playerClients)
		{
			if (!client.IsLoaded() || client.GetPosition().CalculateDistance(Position) > 500)
				continue;

			client.Send(pck, default).Wait();
		}

	}

	private bool Roam(uint serverTick)
	{
		Random rnd = new Random();
		if (_actionStartTick < (20 + rnd.Next(0, 100)))
		{
			_actionStartTick++;
			return false;
		}

		do { _destination = _originalPosition.GetRandomPositionWithinRadius(250);} while (_destination.Compare(Position));

		Direction = Position.CalculateDirection(_destination);
		IsMoving = true;
		_actionStartTick = serverTick;
		return true;
	}

	private byte[] GetMovePacket(FPOS currentPosition, uint serverTick, MOVE_TYPE move_type)
	{
		var vel = GetVelocities();
		SC_MOVE sc_move = new()
		{
			objInstID = Id,
			pos = currentPosition,
			dir = Direction,
			deltaLookAtRadian = 0,
			tick = serverTick,
			moveType = (byte)move_type,
			turningSpeed = 1,
			accel = 0,
			velocity = move_type == MOVE_TYPE.MOVE_TYPE_WALK ? vel.walk : vel.run,
			velocityRatio = 1
		};

		using PacketWriter pw = new(false);
		pw.Write(sc_move);
		return pw.ToPacket();
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
}
