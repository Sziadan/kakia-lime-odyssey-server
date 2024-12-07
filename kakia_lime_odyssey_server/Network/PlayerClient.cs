using kakia_lime_odyssey_logging;
using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.Enums;
using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Database;
using kakia_lime_odyssey_server.Interfaces;
using kakia_lime_odyssey_server.Models;
using kakia_lime_odyssey_server.Models.PcStatusXML;
using System.Threading;

namespace kakia_lime_odyssey_server.Network;

public class PlayerClient : IPlayerClient, IEntity
{
	public Func<PlayerClient, byte[], CancellationToken, Task>? SendGlobal;
	public Func<PlayerClient, CancellationToken, Task>? RequestZonePresence;
	public Func<long, COMMON_STATUS>? RequestStatus;
	public Func<INPC, bool>? AddNPC;

	private SocketClient _socketClient { get; set; }
	private int _clientRevision { get; set; }
	private string _accountId { get; set; }
	private ModClientPC _currentCharacter { get; set; }
	private uint _objInstID = 0;
	private long _target = 0;
	private bool _inCombat = false;
	private int _jobId = 1;

	private WorldPosition _worldPosition = new();
	private VELOCITIES _velocities;
	private ModCommonStatus _status;

	private PlayerInventory _inventory { get; set; } = new();
	private PlayerEquips _equipment { get; set; } = new();

	private bool _inMotion = false;
	private ulong _lastTick = 0;

	private List<uint> _knownPc { get; set; }

	private bool _isLoaded;

	public PlayerClient(SocketClient socketClient)
	{
		_velocities = new()
		{
			ratio = 1.0F,
			walk = 80,
			walkAccel = 0,
			backSwim = 90,
			backSwimAccel = 0,
			backwalkAccel = 0,
			backwalk = 70,			
			run = 90,
			runAccel = 0,
			swim = 90,
			swimAccel = 0
		};
		_knownPc = new List<uint>();

		_accountId = string.Empty;
		_socketClient = socketClient;
		_socketClient.PacketReceived += PacketRecieved;
		Logger.Log($"Client connected [{_socketClient.GetIP()}]");
		_= _socketClient.BeginRead();
	}

	public void Save()
	{
		if (!_isLoaded) return;
		JsonDB.SaveWorldPosition(_accountId, _currentCharacter.appearance.name, _worldPosition);
		JsonDB.SavePlayerInventory(_accountId, _currentCharacter.appearance.name, _inventory);
		JsonDB.SavePlayerEquipment(_accountId, _currentCharacter.appearance.name, _equipment);
	}

	public void Load()
	{
		_worldPosition = JsonDB.GetWorldPosition(_accountId, _currentCharacter.appearance.name);
		_inventory = JsonDB.GetPlayerInventory(_accountId, _currentCharacter.appearance.name);
		_equipment = JsonDB.GetPlayerEquipment(_accountId, _currentCharacter.appearance.name);
		_currentCharacter.appearance.equiped = new ModEquipped(_equipment.Combat.GetEquipped());
	}

	public int GetClientRevision() => _clientRevision;

	public void SetClientRevision(int rev)
	{
		_clientRevision = rev;
	}

	public bool IsConnected()
	{
		return _socketClient.IsAlive;
	}

	public bool IsLoaded()
	{
		return _isLoaded;
	}

	public void SetUnloaded()
	{
		_isLoaded = false;
	}

	public void SetLoaded()
	{
		_isLoaded = true;
	}

	public async Task PacketRecieved(RawPacket packet)
	{
		PacketHandler handler = kakia_lime_odyssey_network.Handler.PacketHandlers.GetHandlerFor(packet.PacketId);
		//Logger.Log($"Recieved [{packet.PacketId}]", LogLevel.Debug);
		if (handler != null)
		{
			try
			{
				handler.HandlePacket(this, packet);
				return;
			}
			catch (Exception e)
			{
				Logger.Log(e);
			}
		}
		else
		{
			Logger.Log($"NOT IMPLEMENTED [{packet.PacketId}]", LogLevel.Warning);
			//Logger.LogPck(packet.Payload);
		}
	}

	public async Task<bool> Send(byte[] packet, CancellationToken token)
	{
		//Logger.Log($"Sending [{((PacketType)BitConverter.ToUInt16(packet, 0))}]", LogLevel.Debug);
		await _socketClient!.Send(packet);
		return true;
	}

	public async Task<bool> SendGlobalPacket(byte[] packet, CancellationToken token)
	{
		await SendGlobal!.Invoke(this, packet, token);
		return true;
	}

	public async Task<bool> Send(PacketType header, byte[] packet, CancellationToken token)
	{
		byte[] bytes = new byte[packet.Length + 4];
		Buffer.BlockCopy(packet, 0, bytes, 4, packet.Length);
		Buffer.BlockCopy(BitConverter.GetBytes((ushort)header), 0, bytes, 0, 2);
		await _socketClient!.Send(bytes);
		return true;
	}

	public void SetAccountId(string accountId)
	{
		_accountId = accountId;
	}

	public string GetAccountId()
	{
		return _accountId;
	}

	public uint GetObjInstID()
	{
		if (_objInstID == 0)
			Logger.Log("Requesting obj id before its set!", LogLevel.Error);

		return _objInstID;
	}

	public void SetCurrentCharacter(CLIENT_PC pc)
	{
		_currentCharacter = new ModClientPC(pc);
		//var hash = MD5.HashData(Encoding.UTF8.GetBytes(pc.appearance.name + Guid.NewGuid()));
		//_objInstID = BitConverter.ToUInt32(hash);

		Random rnd = new Random();
		_objInstID = (uint)(20000 + rnd.Next(0, 9999));

		_status = new()
		{
			lv = pc.status.combatJob.lv,
			mhp = pc.status.hp,
			hp = pc.status.hp,
			mmp = pc.status.mp,
			mp = pc.status.mp			
		};
		
		Load();
		SetEquipToCurrentJob();
	}

	public void ChangeJob(int jobId)
	{
		_jobId = jobId;
		SetEquipToCurrentJob();
	}

	public void SetEquipToCurrentJob()
	{
		switch(_jobId)
		{
			case 1:
				_currentCharacter.appearance.equiped = new ModEquipped(_equipment.Combat.GetEquipped());
				break;

			default:
				_currentCharacter.appearance.equiped = new ModEquipped(_equipment.Life.GetEquipped());
				break;
		}
	}

	private BaseStats CombatBaseStats()
	{
		return new BaseStats()
		{
			Strength = _currentCharacter.status.combatJob.strength,
			Intelligence = _currentCharacter.status.combatJob.intelligence,
			Dexterity = _currentCharacter.status.combatJob.dexterity,
			Agility = _currentCharacter.status.combatJob.agility,
			Vitality = _currentCharacter.status.combatJob.vitality,
			Spirit = _currentCharacter.status.combatJob.spirit,
			Lucky = _currentCharacter.status.combatJob.lucky
		};
	}

	private AttackStatus GetMeleeAttack()
	{
		// Factor in gear here later too..
		var stats = CombatBaseStats();
		var level = _currentCharacter.status.combatJob.lv;
		var weapon = GetEquipment(true).GetItemInSlot(EQUIP_SLOT.MAIN_EQUIP) as Item;

		return new AttackStatus()
		{
			WeaponTypeId = weapon is null ? 0 : (uint)weapon.WeaponType,
			Atk = (ushort) (3 * ((level * 5) + (stats.Strength + (stats.Strength / 5)) + (stats.Dexterity / 8) + (stats.Lucky / 10))),
			CritRate = (ushort)(1 + (stats.Lucky/5)),
			Def = (ushort)((level * 3) + (stats.Vitality + (stats.Vitality/5))),
			Hit = (ushort)((level * 2) + (stats.Dexterity + (stats.Dexterity / 5))),
			FleeRate = (ushort)((level * 3) + (stats.Agility + (stats.Agility / 5)))
		};
	}

	private AttackStatus GetSpellAttack()
	{
		// Factor in gear here later too..
		var stats = CombatBaseStats();
		var level = _currentCharacter.status.combatJob.lv;
		var weapon = GetEquipment(true).GetItemInSlot(EQUIP_SLOT.MAIN_EQUIP) as Item;

		return new AttackStatus()
		{
			WeaponTypeId = weapon is null ? 0 : (uint)weapon.WeaponType,
			Atk = (ushort)((level * 5) + (stats.Intelligence + (stats.Intelligence / 5)) + (stats.Dexterity / 8) + (stats.Spirit / 10)),
			CritRate = (ushort)(1 + (stats.Lucky / 5)),
			Def = (ushort)((level * 3) + (stats.Vitality + (stats.Vitality / 5))),
			Hit = (ushort)((level * 2) + (stats.Dexterity + (stats.Dexterity / 5))),
			FleeRate = (ushort)((level * 3) + (stats.Agility + (stats.Agility / 5)))
		};
	}

	private BasicStatus GetBasicStatus()
	{
		return new BasicStatus()
		{
			Hp = _status.hp,
			MaxHp = _status.mhp,
			Mp = _status.mp,
			MaxMp = _status.mmp
		};
	}

	public FPOS GetPosition()
	{
		return _worldPosition.Position;
	}

	public FPOS GetDirection()
	{
		return _worldPosition.Direction;
	}

	public void UpdatePosition(FPOS pos)
	{
		_worldPosition.Position = pos;
	}

	public void UpdateDirection(FPOS dir)
	{
		_worldPosition.Direction = dir;
	}

	public uint GetZone()
	{
		return _worldPosition.ZoneID;
	}

	public void SetZone(uint zone)
	{
		_worldPosition.ZoneID = zone;
	}

	public ModClientPC GetCurrentCharacter()
	{
		return _currentCharacter;
	}

	public REGION_PC GetRegionPC()
	{
		//_worldPosition.Position = new FPOS() { x = 1000, y = 1000, z = 850 };
		return new()
		{
			objInstID = _objInstID,
			pos = _worldPosition.Position,
			dir = _worldPosition.Direction,
			deltaLookAtRadian = 2,
			status = new()
			{
				commonStatus = _status.AsStruct(),
				lp = _currentCharacter.status.lp,
				mlp = _currentCharacter.status.lp,
				streamPoint = _currentCharacter.status.streamPoint,
				meleeHitRate = 1,
				dodge = 1,
				meleeAtk = 1,
				meleeDefense = 1,
				spellAtk = 1,
				spellDefense = 1,
				parry = 1,
				block = 1,
				resist = 1,
				criticalRate = 1,
				hitSpeedRatio = 1,
				
				lifeJob = new()
				{
					lv = _currentCharacter.status.lifeJob.lv,
					exp = _currentCharacter.status.lifeJob.exp,
					statusPoint = _currentCharacter.status.lifeJob.statusPoint,
					craft = _currentCharacter.status.lifeJob.craft,
					idea = _currentCharacter.status.lifeJob.idea,
					mind = _currentCharacter.status.lifeJob.mind,
					sense = _currentCharacter.status.lifeJob.sense
				},
				combatJob = new()
				{
					lv = _currentCharacter.status.combatJob.lv,
					exp = _currentCharacter.status.combatJob.exp,
					strength = _currentCharacter.status.combatJob.strength,
					intelligence = _currentCharacter.status.combatJob.intelligence,
					dexterity = _currentCharacter.status.combatJob.dexterity,
					agility = _currentCharacter.status.combatJob.agility,
					vitality = _currentCharacter.status.combatJob.vitality,
					spirit = _currentCharacter.status.combatJob.spirit,
					lucky = _currentCharacter.status.combatJob.lucky
				},
				velocities = _velocities,
				collectSucessRate = 1,
				collectionIncreaseRate = 1,
				makeTimeDecreaseAmount = 1
			},
			name = _currentCharacter.appearance.name,
			raceTypeID = _currentCharacter.appearance.raceTypeID,
			lifeJobTypeID = _currentCharacter.appearance.lifeJobTypeID,
			combatJobTypeID = _currentCharacter.appearance.combatJobTypeID,
			genderType = _currentCharacter.appearance.genderType,
			headType = _currentCharacter.appearance.headType,
			hairType = _currentCharacter.appearance.hairType,
			eyeType = _currentCharacter.appearance.eyeType,
			earType = _currentCharacter.appearance.earType,
			playingJobClass = _currentCharacter.appearance.playingJobClass,
			underwearType = _currentCharacter.appearance.underwearType,
			familyNameType = _currentCharacter.appearance.familyNameType,
			streamPoint = _currentCharacter.status.streamPoint,
			transparent = _currentCharacter.appearance.transparent,
			scale = _currentCharacter.appearance.scale,
			guildName = "Test Guild",
			showHelm = _currentCharacter.appearance.showHelm,
			inventoryGrade = _inventory.Grade,
			skinColorType = _currentCharacter.appearance.skinColorType,
			hairColorType = _currentCharacter.appearance.hairColorType,
			eyeColorType = _currentCharacter.appearance.eyeColorType,
			eyeBrowColorType = _currentCharacter.appearance.eyeBrowColorType
		};
	}

	public SC_ENTER_SIGHT_PC GetEnterSight()
	{
		return new()
		{
			enter_zone = new()
			{
				objInstID = GetObjInstID(),
				pos = GetPosition(),
				dir = GetDirection()
			},
			deltaLookAtRadian = 2,
			appearance = GetCurrentCharacter().appearance.AsStruct(),
			guildName = GetRegionPC().guildName,
			raceRelationState = 1,
			stopType = (byte)STOP_TYPE.STOP_TYPE_GROUND
		};
	}

	public async Task RequestPresence(CancellationToken token)
	{
		await RequestZonePresence!.Invoke(this, token);
	}

	public COMMON_STATUS RequestCommonStatus(long id)
	{
		return RequestStatus!.Invoke(id);
	}

	public bool KnowOf(uint id)
	{
		return _knownPc.Contains(id);
	}

	public void Seen(uint id)
	{
		_knownPc.Add(id);
	}

	public void PcLeft(uint id)
	{
		_knownPc.Remove(id);
	}

	public VELOCITIES GetVELOCITIES()
	{
		return _velocities;
	}

	public void UpdateVELOCITIES(VELOCITIES vel)
	{
		_velocities = vel;
	}

	public void SetInMotion(bool inMotion)
	{
		_inMotion = inMotion;
	}

	public bool IsInMotion()
	{
		return _inMotion;
	}

	public COMMON_STATUS GetStatus()
	{
		return _status.AsStruct();
	}

	public void UpdateStatus(ModCommonStatus status)
	{
		_status = status;
	}

	public void AddNpcOrMob(INPC npc)
	{
		AddNPC!.Invoke(npc);
	}

	public void SetCurrentTarget(long target)
	{
		_target = target;
	}

	public long GetCurrentTarget()
	{
		return _target;
	}

	public void InitCombat()
	{
		_inCombat = true;
	}

	public bool InCombat()
	{
		return _inCombat;
	}

	public void StopCombat()
	{
		_inCombat = false;
	}

	public async Task Update(ulong tick)
	{
		return;
	}

	public IPlayerInventory GetInventory()
	{
		return _inventory;
	}

	public IPlayerEquipment GetEquipment(bool combat)
	{
		return combat ? _equipment.Combat : _equipment.Life;
	}

	public void SendInventory()
	{
		using PacketWriter pw = new(_clientRevision == 345);
		pw.Write(_inventory.AsInventoryPacket());
		Send(pw.ToSizedPacket(), default).Wait();
	}

	public void SendEquipment()
	{
		using PacketWriter pw = new(_clientRevision == 345);
		pw.Write(_equipment.Combat.GetCombatEquipList());
		Send(pw.ToSizedPacket(), default).Wait();
	}

	public long GetId()
	{
		return GetObjInstID();
	}

	public EntityStatus GetEntityStatus()
	{
		switch (_jobId)
		{
			case 1:
				return new EntityStatus()
				{
					Lv = _currentCharacter.status.combatJob.lv,
					Exp = _currentCharacter.status.combatJob.exp,
					BaseStats = CombatBaseStats(),
					BasicStatus = GetBasicStatus(),
					MeleeAttack = GetMeleeAttack(),
					SpellAttack = GetSpellAttack()
				};

			default: // Not entirely sure yet how I wanna handle life jobs here..
				return new EntityStatus()
				{
					Lv = _currentCharacter.status.combatJob.lv,
					Exp = _currentCharacter.status.combatJob.exp,
					BaseStats = CombatBaseStats(),
					BasicStatus = GetBasicStatus(),
					MeleeAttack = GetMeleeAttack(),
					SpellAttack = GetSpellAttack()
				};
		}
	}

	public DamageResult UpdateHealth(int healthChange)
	{
		var newHealth = (_status.hp + healthChange);
		_status.hp = newHealth <= 0 ? 0 : (uint)newHealth;

		return new DamageResult()
		{
			TargetKilled = _status.hp == 0,
			ExpReward = 0
		};
	}

	public bool AddExp(ulong exp)
	{
		var currentLevel = _currentCharacter.status.combatJob.lv;
		_currentCharacter.status.combatJob.exp += (uint)exp;
		var expList = PcStatus.GetEntries();

		if (expList[currentLevel].CombatExp >= _currentCharacter.status.combatJob.exp)
			return false;

		_currentCharacter.status.combatJob.lv++;
		_currentCharacter.status.combatJob.exp = (uint)(_currentCharacter.status.combatJob.exp - expList[currentLevel].CombatExp);
		return true;
	}

	public List<Item> GetLoot()
	{
		throw new NotImplementedException();
	}

	public void Loot(Item item)
	{
		throw new NotImplementedException();
	}
}
