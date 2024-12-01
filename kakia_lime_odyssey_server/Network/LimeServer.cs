using kakia_lime_odyssey_logging;
using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Database;
using kakia_lime_odyssey_server.Models;
using kakia_lime_odyssey_server.Models.MonsterXML;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_server.Network;

public class LimeServer : SocketServer
{
	public static List<XmlMonster> MonsterDB = new List<XmlMonster>();
	public static List<Item> ItemDB = ItemInfo.GetItems();

	public List<PlayerClient> PlayerClients = new();
	public Dictionary<uint, List<NPC>> Npcs = new();
	public Dictionary<uint, List<Monster>> Mobs = new();
	public static DateTime StartTime = DateTime.Now;
	public Config Config { get; set; }

	public BackgroundTask BackgroundTask { get; set; }

	public LimeServer(Config cfg) : base(cfg.ServerIP, cfg.Port)
	{
		Config = cfg;
		var villagers = JsonDB.LoadVillagers();
		foreach(var v in villagers)
		{
			if(!Npcs.ContainsKey(v.ZoneId))
				Npcs.Add(v.ZoneId, new List<NPC>());
			Npcs[v.ZoneId].Add(v);
		}
		Logger.Log("Villagers loaded.", LogLevel.Information);

		MonsterDB.AddRange(MonsterInfo.GetEntries());
		Logger.Log("Monster DB loaded.", LogLevel.Information);

		BackgroundTask = new BackgroundTask(TimeSpan.FromMilliseconds(1000/60));
		BackgroundTask.Run += ServerTick;
		BackgroundTask.Start();
	}

	public ReadOnlySpan<PlayerClient> GetReadonlyPlayers()
	{
		return CollectionsMarshal.AsSpan<PlayerClient>(PlayerClients);
	}


	public void Stop()
	{
		try
		{
			BackgroundTask.StopAsync().Wait();
		} catch { }
	}

	public static uint GetCurrentTick()
	{
		return (uint)DateTime.Now.Subtract(StartTime).TotalMilliseconds;
	}

	private async Task ServerTick()
	{

		await UnloadLoggedOutPCs();
		DetectPlayersEnteringSight();

		// MONSTER STUFF

		foreach (var kv in Mobs)
		{
			foreach(var mob in kv.Value)
			{
				mob.Update(GetCurrentTick(), GetReadonlyPlayers());
			}
		}
	}

	private async Task UnloadLoggedOutPCs()
	{
		List<PlayerClient> remove = new();

		foreach (var pc in PlayerClients)
		{
			if (!pc.IsConnected())
			{
				remove.Add(pc);
				continue;
			}

			await pc.Update(GetCurrentTick());
		}

		foreach (var pc in remove)
		{
			PlayerClients.Remove(pc);

			pc.SendGlobal -= SendGlobal;
			pc.RequestZonePresence -= LoadOthersInZone;
			pc.RequestStatus -= GetStatusFor;
			pc.AddNPC -= AddNPC;
			pc.Save();

			SC_LEAVE_SIGHT_PC leave_pc = new()
			{
				leave_zone = new()
				{
					objInstID = pc.GetObjInstID()
				}
			};

			using PacketWriter pw = new(pc.GetClientRevision() == 345);
			pw.Write(leave_pc);
			await SendGlobal(pc, pw.ToPacket(), default);
		}
	}

	private void DetectPlayersEnteringSight()
	{
		var players = GetReadonlyPlayers();
		foreach (var requester in players)
		{
			if (!requester.IsLoaded())
				continue;

			foreach (var pc in players)
			{
				if (!pc.IsLoaded() || pc == requester || pc.GetZone() != requester.GetZone() || requester.KnowOf(pc.GetObjInstID())) continue;

				requester.Seen(pc.GetObjInstID());

				var loadPC = pc.GetEnterSight();
				using PacketWriter pw = new(requester.GetClientRevision() == 345);
				pw.Write(loadPC);
				requester.Send(pw.ToSizedPacket(), default).Wait();
			}
		}
	}

	public override void OnConnect(SocketClient s)
	{
		s.UseCrypto = Config.Crypto;
		var pc = new PlayerClient(s);
		pc.SendGlobal += SendGlobal;
		pc.RequestZonePresence += LoadOthersInZone;
		pc.RequestStatus += GetStatusFor;
		pc.AddNPC += AddNPC;
		PlayerClients.Add(pc);
	}

	public bool AddNPC(INPC npc)
	{
		uint zone = 0;

		switch(npc.GetNPCType())
		{
			case NPC_TYPE.MOB:
				var mob = npc as Monster;
				zone = mob.Zone;
				if (!Mobs.ContainsKey(zone)) Mobs.Add(zone, new List<Monster>());

				Mobs[zone].Add(mob);
				break;

			case NPC_TYPE.NPC:
				var np = npc as NPC;
				zone = np.ZoneId;
				if (!Npcs.ContainsKey(zone)) Npcs.Add(zone, new List<NPC>());
				Npcs[zone].Add(np);
				break;
		}

		foreach(PlayerClient pc in GetReadonlyPlayers())
		{
			if (!pc.IsLoaded() || pc.GetZone() != zone)
				continue;

			using PacketWriter pw = new(pc.GetClientRevision() == 345);
			switch (npc.GetNPCType())
			{
				case NPC_TYPE.MOB:
					pw.Write((npc as Monster).GetEnterSight());
					break;

				case NPC_TYPE.NPC:
					pw.Write((npc as NPC).GetEnterSight());
					break;
			}
			pc.Send(pw.ToSizedPacket(), default).Wait();
		}

		return true;
	}

	public COMMON_STATUS GetStatusFor(long objInstID)
	{
		var player = PlayerClients.FirstOrDefault(pc => pc.GetObjInstID() == (uint)objInstID);
		if (player is not null)
			return player.GetStatus();

		foreach(var kv in Npcs)
		{
			var npc = kv.Value.FirstOrDefault(n => n.Id == (uint)objInstID);
			if (npc is not null)
				return npc.Status;
		}

		foreach (var kv in Mobs)
		{
			var mob = kv.Value.FirstOrDefault(m => m.Id == (uint)objInstID);
			if (mob is not null)
				return mob.GetMob().Status;
		}

		return new COMMON_STATUS();
	}

	public async Task LoadOthersInZone(PlayerClient requester, CancellationToken token)
	{
		if (Npcs.ContainsKey(requester.GetZone()))
		{
			foreach(var npc in Npcs[requester.GetZone()].Where(n => !requester.KnowOf((uint)n.Id)))
			{
				var loadNPC = npc.GetEnterSight();
				using PacketWriter pw = new(requester.GetClientRevision() == 345);
				pw.Write(loadNPC);
				await requester.Send(pw.ToSizedPacket(), token);
			}
		}

		if (Mobs.ContainsKey(requester.GetZone()))
		{
			foreach (var mob in Mobs[requester.GetZone()].Where(n => !requester.KnowOf((uint)n.Id)))
			{
				var loadMob = mob.GetEnterSight();
				using PacketWriter pw = new(requester.GetClientRevision() == 345);
				pw.Write(loadMob);
				await requester.Send(pw.ToSizedPacket(), token);
			}
		}
	}

	public async Task SendGlobal(PlayerClient sender, byte[] packet, CancellationToken token)
	{
		foreach(var pc in GetReadonlyPlayers())
		{
			if (pc == sender || pc.GetZone() != sender.GetZone()) continue;
			pc.Send(packet, token).Wait();
		}
	}
}
