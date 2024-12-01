using kakia_lime_odyssey_packets.Packets.Interface;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential)]
public struct SC_LEAVE_SIGHT_PC : IPacketFixed
{
	public SC_LEAVE_ZONEOBJ leave_zone;
}

[StructLayout(LayoutKind.Sequential)]
public struct SC_LEAVE_SIGHT_MONSTER : IPacketFixed
{
	public SC_LEAVE_ZONEOBJ leave_zone;
}

[StructLayout(LayoutKind.Sequential)]
public struct SC_LEAVE_SIGHT_VILLAGER : IPacketFixed
{
	public SC_LEAVE_ZONEOBJ leave_zone;
}

[StructLayout(LayoutKind.Sequential)]
public struct SC_LEAVE_SIGHT_QUEST_BOARD : IPacketFixed
{
	public SC_LEAVE_ZONEOBJ leave_zone;
}

[StructLayout(LayoutKind.Sequential)]
public struct SC_LEAVE_SIGHT_PROP : IPacketFixed
{
	public SC_LEAVE_ZONEOBJ leave_zone;
}

[StructLayout(LayoutKind.Sequential)]
public struct SC_LEAVE_SIGHT_MERCHANT : IPacketFixed
{
	public SC_LEAVE_ZONEOBJ leave_zone;
}

[StructLayout(LayoutKind.Sequential)]
public struct SC_LEAVE_SIGHT_TRANSFER : IPacketFixed
{
	public SC_LEAVE_ZONEOBJ leave_zone;
}

[StructLayout(LayoutKind.Sequential)]
public struct SC_LEAVE_SIGHT_HOUSE : IPacketFixed
{
	public SC_LEAVE_ZONEOBJ leave_zone;
}

[StructLayout(LayoutKind.Sequential)]
public struct SC_LEAVE_SIGHT_BULLET_SKILL : IPacketFixed
{
	public SC_LEAVE_ZONEOBJ leave_zone;
}

[StructLayout(LayoutKind.Sequential)]
public struct SC_LEAVE_SIGHT_BULLET_ITEM : IPacketFixed
{
	public SC_LEAVE_ZONEOBJ leave_zone;
}