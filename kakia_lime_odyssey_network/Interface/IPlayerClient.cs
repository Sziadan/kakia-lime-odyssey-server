using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Models;
using System.ComponentModel;

namespace kakia_lime_odyssey_network.Interface;

public interface IPlayerClient
{
	public Task<bool> Send(byte[] packet, CancellationToken token);
	public Task<bool> Send(PacketType header, byte[] packet, CancellationToken token);
	public Task<bool> SendGlobalPacket(byte[] packet, CancellationToken token);

	public bool IsConnected();

	public bool IsLoaded();
	public void SetUnloaded();
	public void SetLoaded();
	public void SetEquipToCurrentJob();
	public void ChangeJob(int jobId);

	public int GetClientRevision();
	public void SetClientRevision(int rev);

	public void SetAccountId(string accountId);
	public string GetAccountId();
	public uint GetObjInstID();
	public void SetCurrentCharacter(CLIENT_PC pc);
	public void SetCurrentTarget(long target);
	public long GetCurrentTarget();

	public void InitCombat();
	public bool InCombat();
	public void StopCombat();

	public IPlayerInventory GetInventory();
	public IPlayerEquipment GetEquipment(bool combat);

	public void SendInventory();
	public void SendEquipment();

	public FPOS GetPosition();
	public FPOS GetDirection();	
	public void UpdatePosition(FPOS pos);
	public void UpdateDirection(FPOS dir);

	public VELOCITIES GetVELOCITIES();
	public void UpdateVELOCITIES(VELOCITIES vel);

	public void SetInMotion(bool inMotion);
	public bool IsInMotion();

	public uint GetZone();
	public void SetZone(uint zone);

	public ModClientPC GetCurrentCharacter();
	public REGION_PC GetRegionPC();
	public SC_ENTER_SIGHT_PC GetEnterSight();
	public Task RequestPresence(CancellationToken token);

	public COMMON_STATUS GetStatus();
	public COMMON_STATUS RequestCommonStatus(long id);

	public bool KnowOf(uint id);
	public void Seen(uint id);
	public void PcLeft(uint id);
	public void AddNpcOrMob(INPC npc);
}
