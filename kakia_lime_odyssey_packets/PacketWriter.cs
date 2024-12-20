﻿using kakia_lime_odyssey_packets.Packets.Interface;
using kakia_lime_odyssey_packets.Packets.SC;
using System.Text;

namespace kakia_lime_odyssey_packets;

public class PacketWriter : BinaryWriter
{
	private bool _rev345 { get; set; }
	public PacketWriter(bool rev345) : base(new MemoryStream()) { _rev345 = rev345; }

	public void Write(PacketHeader header)
	{
		Write((ushort)header.Type);
		Write(header.Size);
	}

	public void WriteHeader(PacketType type)
	{
		Write((ushort)type);
		Write((ushort)0);
	}

	public void WriteHeader(PacketType_REV345 type)
	{
		Write((ushort)type);
		Write((ushort)0);
	}

	public void Write(IPacketVar packet)
	{
		var objType = packet.GetType();
		if (_rev345)
		{
			PacketType_REV345 type = (PacketType_REV345)Enum.Parse(typeof(PacketType_REV345), objType.Name);
			WriteHeader(type);
		}
		else
		{
			PacketType type = (PacketType)Enum.Parse(typeof(PacketType), objType.Name);
			WriteHeader(type);
		}
		Write(PacketConverter.AsBytes(packet));
	}

	public void Write(IPacketFixed packet)
	{
		var objType = packet.GetType();
		if (!_rev345)
		{
			PacketType type = (PacketType)Enum.Parse(typeof(PacketType), objType.Name);
			Write((ushort)type);
		}
		else
		{
			PacketType_REV345 type = (PacketType_REV345)Enum.Parse(typeof(PacketType_REV345), objType.Name);
			Write((ushort)type);
		}
		
		Write(PacketConverter.AsBytes(packet));
	}

	public void Write(SC_PC_LIST sc_pc_list)
	{
		if (_rev345) WriteHeader(PacketType_REV345.SC_PC_LIST);
		else WriteHeader(PacketType.SC_PC_LIST);

		Write(sc_pc_list.count);
		foreach (var pc in sc_pc_list.pc_list)
			Write(PacketConverter.AsBytes(pc));
	}

	public void Write(SC_MOVE_SLOT_ITEM sc_move_item)
	{
		if (_rev345) WriteHeader(PacketType_REV345.SC_MOVE_SLOT_ITEM);
		else WriteHeader(PacketType.SC_MOVE_SLOT_ITEM);

		foreach (var move_item in sc_move_item.move_list)		
			Write(PacketConverter.AsBytes(move_item));		
	}

	public void Write(SC_TALKING sc_talking)
	{
		if (_rev345) WriteHeader(PacketType_REV345.SC_TALKING);
		else WriteHeader(PacketType.SC_TALKING);

		Write(sc_talking.objInstID);
		FixAlign(4);
		Write(Encoding.ASCII.GetBytes(sc_talking.dialog));
		Write((byte)0);
	}

	public void Write(SC_INVENTORY_ITEM_LIST sc_inventory)
	{
		if (_rev345) WriteHeader(PacketType_REV345.SC_INVENTORY_ITEM_LIST);
		else WriteHeader(PacketType.SC_INVENTORY_ITEM_LIST);

		Write(sc_inventory.maxCount);
		Write(sc_inventory.inventoryGrade);
		foreach (var item in sc_inventory.inventory)
			Write(PacketConverter.AsBytes(item));
	}

	public void Write(SC_LOOTABLE_ITEM_LIST sc_lootable)
	{
		if (_rev345) WriteHeader(PacketType_REV345.SC_LOOTABLE_ITEM_LIST);
		else WriteHeader(PacketType.SC_LOOTABLE_ITEM_LIST);

		Write((int)sc_lootable.count);		
		foreach (var item in sc_lootable.lootTable)
			Write(PacketConverter.AsBytes(item));
	}

	public void Write(SC_COMBAT_JOB_EQUIP_ITEM_LIST sc_combat_equip_item_list)
	{
		if (_rev345) WriteHeader(PacketType_REV345.SC_COMBAT_JOB_EQUIP_ITEM_LIST);
		else WriteHeader(PacketType.SC_COMBAT_JOB_EQUIP_ITEM_LIST);

		foreach (var item in sc_combat_equip_item_list.equipList)
			Write(PacketConverter.AsBytes(item));
	}

	public void Write(SC_LIFE_JOB_EQUIP_ITEM_LIST sc_life_equip_item_list)
	{
		if (_rev345) WriteHeader(PacketType_REV345.SC_LIFE_JOB_EQUIP_ITEM_LIST);
		else WriteHeader(PacketType.SC_LIFE_JOB_EQUIP_ITEM_LIST);

		foreach (var item in sc_life_equip_item_list.equipList)
			Write(PacketConverter.AsBytes(item));
	}

	public void Write(SC_COMBAT_JOB_EQUIPMENT_LIST sc_equipment)
	{
		if (_rev345) WriteHeader(PacketType_REV345.SC_COMBAT_JOB_EQUIPMENT_LIST);
		else WriteHeader(PacketType.SC_COMBAT_JOB_EQUIPMENT_LIST);

		foreach (var item in sc_equipment.equips)
			Write(PacketConverter.AsBytes(item));
	}

	public void Write(SC_LIFE_JOB_EQUIPMENT_LIST sc_equipment)
	{
		if (_rev345) WriteHeader(PacketType_REV345.SC_LIFE_JOB_EQUIPMENT_LIST);
		else WriteHeader(PacketType.SC_LIFE_JOB_EQUIPMENT_LIST);

		foreach (var item in sc_equipment.equips)
			Write(PacketConverter.AsBytes(item));
	}

	public void Write(SC_SAY say)
	{
		if (_rev345) WriteHeader(PacketType_REV345.SC_SAY);
		else WriteHeader(PacketType.SC_SAY);

		Write(say.objInstID);
		Write(say.maintainTime);
		Write(say.type);
		FixAlign(4);
		Write(Encoding.ASCII.GetBytes(say.message));
		Write((byte)0);
	}

	public void Write(SC_QUEST_LIST sc_quest_list)
	{
		if (_rev345) WriteHeader(PacketType_REV345.SC_QUEST_LIST);
		else WriteHeader(PacketType.SC_QUEST_LIST);

		Write(sc_quest_list.details.Count);
		Write(sc_quest_list.completedMain);
		Write(sc_quest_list.completedSub);
		Write(sc_quest_list.completedNormal);
		foreach (var quest in sc_quest_list.details)
		{
			Write(quest.questId);
			Write((byte)quest.questState);			
			Write(Encoding.ASCII.GetBytes(quest.questDescription));
			Write((byte)0);
		}
	}

	public void Write(SC_SKILL_LIST sc_skill_list)
	{
		if (_rev345) WriteHeader(PacketType_REV345.SC_SKILL_LIST);
		else WriteHeader(PacketType.SC_SKILL_LIST);

		foreach(var skill in sc_skill_list.skills)
			Write(PacketConverter.AsBytes(skill));
	}

	public void FixAlign(int align = 4)
	{
		int padding = (int)(align - (this.BaseStream.Position % 4));
		if (padding > 0 && padding != align)
			WriteBytes(0, (uint)padding);
	}

	public void WriteBytes(byte b, uint count)
	{
		for (int i = 0; i < count; i++)
		{
			Write(b);
		}
	}

	public byte[] ToSizedPacket()
	{
		var packet = ((MemoryStream)BaseStream).ToArray();
		short len = (short)(packet.Length);

		Buffer.BlockCopy(BitConverter.GetBytes(len), 0, packet, 2, 2);
		return packet;
	}

	public byte[] ToPacket()
	{
		return ((MemoryStream)BaseStream).ToArray();
	}
}
