using kakia_lime_odyssey_logging;
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
	
	public PlayerClient? CurrentTarget { get; set; }
	public bool Aggro { get; set; }

	private FPOS _destination;
	private FPOS _originalPosition;

	private uint _actionStartTick;
	private uint _lastTick;
	public bool IsMoving;

	private MOVE_TYPE _moveType = MOVE_TYPE.MOVE_TYPE_WALK;

	private readonly uint update_distance = 1500;

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

		CurrentTarget = null;
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

		bool noTarget = CurrentTarget is null;

		if (IsPlayerWithinAggroZone(playerClients))
		{
			if (noTarget)
			{
				Position = Position.CalculateCurrentPosition(_destination, GetCurrentVelocity(), (double)((serverTick - _actionStartTick) / 1000));
				_actionStartTick = serverTick;
			}

			ChaseAndAttackPlayer(serverTick, playerClients);
		}

		if (!IsMoving)
		{
			if (CurrentTarget == null)
			{
				if (!Roam(serverTick))
					return;
			}

			// Turn the monster straight away
			var stop = GetStopPacket(Position, serverTick);
			foreach (var client in playerClients)
			{
				if (!client.IsLoaded() || client.GetPosition().CalculateDistance(Position) > update_distance)
					continue;

				client.Send(stop, default).Wait();
			}

			if (CurrentTarget != null)
				return;
		}

		
		var currentPosition = Position.CalculateCurrentPosition(_destination, GetCurrentVelocity(), (double)((serverTick - _actionStartTick)/1000));
		if (float.IsNaN(currentPosition.x) || float.IsNaN(currentPosition.y) || float.IsNaN(currentPosition.z))
			currentPosition = Position;


		if (currentPosition.Compare(_destination) || (CurrentTarget is not null && ReachedPlayer(currentPosition)))
		{
			Position = currentPosition;
			_destination = default;


			IsMoving = false;
			_actionStartTick = 0;

			var sc_stop = GetStopPacket(Position, serverTick);

			foreach (var client in playerClients)
			{
				if (!client.IsLoaded() || client.GetPosition().CalculateDistance(Position) > update_distance)
					continue;

				client.Send(sc_stop, default).Wait();
			}
			return;
		}

		var pck = GetMovePacket(currentPosition, serverTick);
		foreach (var client in playerClients)
		{
			if (!client.IsLoaded() || client.GetPosition().CalculateDistance(Position) > update_distance)
				continue;

			client.Send(pck, default).Wait();
		}
	}

	private void ChaseAndAttackPlayer(uint serverTick, ReadOnlySpan<PlayerClient> playerClients)
	{
		if (CurrentTarget != null)
		{
			var currentPosition = Position.CalculateCurrentPosition(_destination, GetCurrentVelocity(), (double)((serverTick - _actionStartTick) / 1000));

			var distance = CurrentTarget.GetPosition().CalculateDistance(currentPosition);

			_moveType = MOVE_TYPE.MOVE_TYPE_RUN;
			_destination = CurrentTarget.GetPosition();
			if (distance > 0) {
				Direction = currentPosition.CalculateDirection(CurrentTarget.GetPosition());
			}

			// No need to get closer
			if (ReachedPlayer(currentPosition))
			{
				_destination = currentPosition;
			}
			else if(!IsMoving)
			{
				IsMoving = true;
				_actionStartTick = serverTick;
			}
		}
	}

	private bool ReachedPlayer(FPOS position)
	{
		if (CurrentTarget == null)
			return false;

		return CurrentTarget.GetPosition().CalculateDistance(position) < 1;
	}

	private bool IsPlayerWithinAggroZone(ReadOnlySpan<PlayerClient> playerClients)
	{
		return false;
		int aggro_range = 150;

		if (CurrentTarget != null)
		{
			if (CurrentTarget.GetPosition().CalculateDistance(Position) < aggro_range)
				return true;

			CurrentTarget = null;
		}

		double closest = double.MaxValue;
		foreach (var client in playerClients)
		{
			var distance = client.GetPosition().CalculateDistance(Position);
			if (distance > aggro_range)
				continue;

			if (closest < distance)
				continue;

			closest = distance;
			CurrentTarget = client;
		}

		if (closest == double.MaxValue)
		{
			CurrentTarget = null;
			_moveType = MOVE_TYPE.MOVE_TYPE_WALK;
			if (!IsMoving)
				_destination = default;
		}

		return CurrentTarget != null;
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
}
