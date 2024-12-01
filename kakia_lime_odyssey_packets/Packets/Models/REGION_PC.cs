using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct REGION_PC
{
	public long objInstID;
	public FPOS pos;
	public FPOS dir;
	public float deltaLookAtRadian;
	public STATUS_PC status;
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
	public byte familyNameType;
	public uint streamPoint;
	public float transparent;
	public float scale;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
	public string guildName;
	[MarshalAs(UnmanagedType.U1)]
	public bool showHelm;
	public byte inventoryGrade;
	public byte skinColorType;
	public byte hairColorType;
	public byte eyeColorType;
	public byte eyeBrowColorType;
}
