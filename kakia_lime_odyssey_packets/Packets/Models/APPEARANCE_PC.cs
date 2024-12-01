using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential)]
public struct APPEARANCE_PC_KR
{
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 26)]
	public string name;
	public uint raceTypeID;
	public byte lifeJobTypeID;
	public byte combatJobTypeID;
	[MarshalAs(UnmanagedType.U1)]
	public bool genderType;
	public byte headType;
	public byte hairType;
	public byte eyeType;
	public byte earType;
	public byte playingJobClass;
	public short underwearType;
	public EQUIPPED equiped { get; set; }
	public byte familyNameType;
	public uint action;
	public uint actionStartTick;
	public float scale;
	public float transparent;
	[MarshalAs(UnmanagedType.U1)]
	public bool showHelm;
	public COLOR color;
	public byte skinColorType;
	public byte hairColorType;
	public byte eyeColorType;
	public byte eyeBrowColorType;
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct APPEARANCE_PC
{
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 26)]
	public string name;
	public byte raceTypeID;
	public byte lifeJobTypeID;
	public byte combatJobTypeID;
	[MarshalAs(UnmanagedType.U1)]
	public bool genderType;
	public byte headType;
	public byte hairType;
	public byte eyeType;
	public byte earType;
	public byte playingJobClass;
	public byte unk;
	public short underwearType;
	public int unk1;
	public byte unk2;
	public EQUIPPED equiped;
	public byte familyNameType;
	public uint action;
	public uint actionStartTick;
	public float scale;
	public float transparent;
	[MarshalAs(UnmanagedType.U1)]
	public bool showHelm;
	public COLOR color;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
	public byte[] unk3;

	public byte skinColorType;
	public byte hairColorType;
	public byte eyeColorType;
	public byte eyeBrowType;
	public byte eyeBrowColorType;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
	public byte[] unk4;
}


public class ModAppearance
{
	public string name;
	public uint raceTypeID;
	public byte lifeJobTypeID;
	public byte combatJobTypeID;
	public bool genderType;
	public byte headType;
	public byte hairType;
	public byte eyeType;
	public byte earType;
	public byte playingJobClass;
	public short underwearType;
	public ModEquipped equiped { get; set; }
	public byte familyNameType;
	public uint action;
	public uint actionStartTick;
	public float scale;
	public float transparent;
	public bool showHelm;
	public COLOR color;
	public byte skinColorType;
	public byte hairColorType;
	public byte eyeColorType;
	public byte eyeBrowColorType;


	public ModAppearance(APPEARANCE_PC_KR other)
	{
		this.name = other.name;
		this.raceTypeID = other.raceTypeID;
		this.lifeJobTypeID = other.lifeJobTypeID;
		this.combatJobTypeID = other.combatJobTypeID;
		this.genderType = other.genderType;
		this.headType = other.headType;
		this.hairType = other.hairType;
		this.eyeType = other.eyeType;
		this.earType = other.earType;
		this.playingJobClass = other.playingJobClass;
		this.underwearType = other.underwearType;
		this.equiped = new ModEquipped(other.equiped);
		this.familyNameType = other.familyNameType;
		this.action = other.action;
		this.actionStartTick = other.actionStartTick;
		this.scale = other.scale;
		this.transparent = other.transparent;
		this.showHelm = other.showHelm;
		this.color = other.color;
		this.skinColorType = other.skinColorType;
		this.hairColorType = other.hairColorType;
		this.eyeColorType = other.eyeColorType;
		this.eyeBrowColorType = other.eyeBrowColorType;
	}

	public APPEARANCE_PC_KR AsStruct()
	{
		return new APPEARANCE_PC_KR()
		{
			name = name,
			raceTypeID = raceTypeID,
			lifeJobTypeID = lifeJobTypeID,
			combatJobTypeID = combatJobTypeID,
			genderType = genderType,
			headType = headType,
			hairType = hairType,
			eyeType = eyeType,
			earType = earType,
			playingJobClass = playingJobClass,
			underwearType = underwearType,
			equiped = equiped.AsStruct(),
			familyNameType = familyNameType,
			action = action,
			actionStartTick = actionStartTick,
			scale = scale,
			transparent = transparent,
			showHelm = showHelm,
			color = color,
			skinColorType = skinColorType,
			hairColorType = hairColorType,
			eyeColorType = eyeColorType,
			eyeBrowColorType = eyeBrowColorType
		};
	}
}