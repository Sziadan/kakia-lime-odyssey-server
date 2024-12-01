using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.CS;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CS_CREATE_PC_KR
{
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 26)]
	public string name;
	public uint raceTypeID;
	[MarshalAs(UnmanagedType.U1)]
	public bool genderType;
	public byte lifeJobTypeID;
	public byte headType;
	public byte hairType;
	public byte eyeType;
	public byte earType;
	public short underwearType;
	public byte familyNameType;
	public byte skinColorType;
	public byte hairColorType;
	public byte eyeColorType;
	public byte eyeBrowColorType;
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CS_CREATE_PC
{
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 26)]
	public string name;
	public byte raceTypeID;
	[MarshalAs(UnmanagedType.U1)]
	public bool genderType;
	public byte lifeJobTypeID;
	public byte combatJobTypeID;
	public byte headType;
	public byte hairType;
	public byte eyeType;
	public byte earType;
	public short underwearType;
	public byte skinColorType;
	public byte hairColorType;
	public byte eyeColorType;
	public byte eyeBrowType;
	public byte eyeBrowColorType;
}

