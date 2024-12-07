using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_server.Models;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace kakia_lime_odyssey_server.Database;

public static class JsonDB
{
	private static void CheckMakeDBFolder()
	{
		CheckMakePath("db");
	}

	private static void CheckCharacter(string accountId, string character)
	{
		string path = Path.Combine("db", accountId);
		CheckMakePath(path);
		path = Path.Combine(path, character);
		CheckMakePath(path);
	}

	private static void CheckMakePath(string path)
	{
		if (!Directory.Exists(path))
			Directory.CreateDirectory(path);
	}

	public static List<CLIENT_PC> LoadPC(string accountId)
	{
		CheckMakeDBFolder();
		string path = Path.Combine("db", accountId);

		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
			return new();
		}

		List<CLIENT_PC> pc_list = new();

		var characters = Directory.GetDirectories(path);
		foreach (var character in characters)
		{
			var dir = new DirectoryInfo(character).Name;
			pc_list.Add(new CLIENT_PC()
			{
				appearance = GetAppearance(accountId, dir),
				status = GetSavedStatusPC(accountId, dir)
			});
		}

		return pc_list;
	}

	public static PlayerInventory GetPlayerInventory(string accountId, string character)
	{
		string path = Path.Combine("db", accountId, character, "inventory.json");
		if (!File.Exists(path))
		{
			var nw = new PlayerInventory();
			SavePlayerInventory(accountId, character, nw);
			return nw;
		}
		return JsonConvert.DeserializeObject<PlayerInventory>(File.ReadAllText(path))!;
	}

	public static void SavePlayerInventory(string accountId, string character, PlayerInventory inventory)
	{
		CheckCharacter(accountId, character);
		string path = Path.Combine("db", accountId, character, "inventory.json");
		File.WriteAllText(path, JsonConvert.SerializeObject(inventory));
	}

	public static PlayerEquips GetPlayerEquipment(string accountId, string character)
	{
		string path = Path.Combine("db", accountId, character, "equipment.json");
		if (!File.Exists(path))
		{
			var nw = new PlayerEquips();
			SavePlayerEquipment(accountId, character, nw);
			return nw;
		}
		return JsonConvert.DeserializeObject<PlayerEquips>(File.ReadAllText(path))!;
	}

	public static void SavePlayerEquipment(string accountId, string character, PlayerEquips equipment)
	{
		CheckCharacter(accountId, character);
		string path = Path.Combine("db", accountId, character, "equipment.json");
		File.WriteAllText(path, JsonConvert.SerializeObject(equipment));
	}

	public static WorldPosition GetWorldPosition(string accountId, string character)
	{
		string path = Path.Combine("db", accountId, character, "world_position.json");
		if (!File.Exists(path))
		{
			var nw = new WorldPosition();
			SaveWorldPosition(accountId, character, nw);
			return nw;
		}
		return JsonConvert.DeserializeObject<WorldPosition>(File.ReadAllText(path))!;
	}

	public static void SaveWorldPosition(string accountId, string character, WorldPosition worldPosition)
	{
		CheckCharacter(accountId, character);
		string path = Path.Combine("db", accountId, character, "world_position.json");
		File.WriteAllText(path, JsonConvert.SerializeObject(worldPosition));
	}

	public static APPEARANCE_PC_KR GetAppearance(string accountId, string character)
	{
		string path = Path.Combine("db", accountId, character, "appearance.json");
		return JsonConvert.DeserializeObject<APPEARANCE_PC_KR>(File.ReadAllText(path));
	}

	public static void StoreAppearance(string accountId, string character, APPEARANCE_PC_KR data)
	{
		CheckCharacter(accountId, character);
		string path = Path.Combine("db", accountId, character, "appearance.json");
		File.WriteAllText(path, JsonConvert.SerializeObject(data));
	}

	public static SAVED_STATUS_PC_KR GetSavedStatusPC(string accountId, string character)
	{
		string path = Path.Combine("db", accountId, character, "saved_status_pc.json");
		return JsonConvert.DeserializeObject<SAVED_STATUS_PC_KR>(File.ReadAllText(path));
	}

	public static void StoreSavedStatusPC(string accountId, string character, SAVED_STATUS_PC_KR data)
	{
		CheckCharacter(accountId, character);
		string path = Path.Combine("db", accountId, character, "saved_status_pc.json");
		File.WriteAllText(path, JsonConvert.SerializeObject(data));
	}

	public static List<NPC> LoadVillagers()
	{
		string path = Path.Combine("db", "NPC_Villagers.json");
		return JsonConvert.DeserializeObject<List<NPC>>(File.ReadAllText(path))!;
	}

	public static List<MapMob> LoadMapMobs()
	{
		string path = Path.Combine("db", "mob_spawns.json");
		return JsonConvert.DeserializeObject<List<MapMob>>(File.ReadAllText(path))!;
	}

	public static Dictionary<int, List<LootableItem>> LoadItemDropTable()
	{
		string path = Path.Combine("db", "loot_table.json");
		return JsonConvert.DeserializeObject<Dictionary<int, List<LootableItem>>>(File.ReadAllText(path))!;
	}
}
