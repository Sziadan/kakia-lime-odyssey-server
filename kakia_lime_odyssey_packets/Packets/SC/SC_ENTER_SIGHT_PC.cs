using kakia_lime_odyssey_packets.Packets.Interface;
using kakia_lime_odyssey_packets.Packets.Models;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 2)]
public struct SC_ENTER_SIGHT_PC : IPacketVar
{
	public SC_ENTER_ZONEOBJ enter_zone;
	public float deltaLookAtRadian;
	public APPEARANCE_PC_KR appearance;
	public int raceRelationState;
	public byte stopType;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
	public string guildName;
}
