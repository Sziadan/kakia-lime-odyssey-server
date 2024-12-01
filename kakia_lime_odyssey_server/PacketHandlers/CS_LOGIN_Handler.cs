using kakia_lime_odyssey_logging;
using kakia_lime_odyssey_network;
using kakia_lime_odyssey_network.Handler;
using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets;
using kakia_lime_odyssey_packets.Packets.CS;
using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;
using kakia_lime_odyssey_server.Database;
using kakia_lime_odyssey_utils.Extensions;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_server.PacketHandlers;

[PacketHandlerAttr(PacketType.CS_LOGIN)]
class CS_LOGIN_Handler : PacketHandler
{
	public override void HandlePacket(IPlayerClient client, RawPacket p)
	{
		var login = PacketConverter.Extract<CS_LOGIN>(p.Payload);
		client.SetAccountId(login.id);
		client.SetClientRevision(login.revision);

		Logger.Log($"Account login [{login.id}:{login.pw}][REV: {login.revision}]");

		var characters = JsonDB.LoadPC(login.id);
		
		var updated = new List<CLIENT_PC>();

		foreach (var character in characters)
		{
			var equip = JsonDB.GetPlayerEquipment(login.id, character.appearance.name);
			var equipped = equip.Combat.GetEquipped();
			var modApp = new ModAppearance(character.appearance);
			modApp.equiped = new ModEquipped(equipped);
			modApp.playingJobClass = 1;

			updated.Add(new CLIENT_PC()
			{
				status = character.status,
				appearance = modApp.AsStruct()
			});
		}
		

		SC_PC_LIST char_list = new()
		{
			count = (byte)characters.Count,
			pc_list = updated.ToArray()
		};



		using PacketWriter pw = new(login.revision == 345);
		pw.Write(char_list);
		var pck = pw.ToSizedPacket();
		//Logger.LogPck(pck);

		/*
		pw.WriteHeader(PacketType_REV345.SC_PC_LIST);
		pw.Write((byte)2); // Count

		// CHAR 1

		pw.Write((byte)0); // Unk
		pw.Write((byte)1); // Life job level
		pw.Write((byte)2); // Combat job level
		pw.Write((byte)0); // Unk

		using PacketWriter pw2 = new(true);

		// Name
		pw2.Write(System.Text.Encoding.ASCII.GetBytes("Sziadan"));
		pw2.WriteBytes(0, 19);

		// Race
		pw2.Write((byte)2);

		// Life job
		pw2.Write((byte)108);

		// Combat job
		pw2.Write((byte)2); 

		// Gender
		pw2.Write((byte)1);

		// Head
		pw2.Write((byte)0);

		// Hair
		pw2.Write((byte)3);

		// Eye (same value as face/head?)
		pw2.Write((byte)0);

		// Ear
		pw2.Write((byte)2);

		// playingJobClass
		pw2.Write((byte)2);

		// Underwear
		pw2.Write((short)-3);
		pw2.WriteBytes(0, 45);

		pw2.WriteBytes(255, 26);
		pw2.WriteBytes(1, 50);
		pw2.WriteBytes(0, 10);

		var appear = pw2.ToPacket();
		var appearstruct = PacketConverter.Extract<APPEARANCE_PC>(appear);

		var new_pc = new CLIENT_PC()
		{
			status = new(),
			appearance = appearstruct
		};
		client.SetCurrentCharacter(new_pc);


		pw.Write(appear);
		*/
		client.Send(pw.ToSizedPacket(), default);
	}
}