using kakia_lime_odyssey_packets.Packets.Interface;
using kakia_lime_odyssey_packets.Packets.Models;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SC_ENTER_SIGHT_MONSTER : IPacketVar
{
	public SC_ENTER_ZONEOBJ enter_zone;
	public APPEARANCE_MONSTER appearance;
	public int raceRelationState;
	public byte stopType;
}
