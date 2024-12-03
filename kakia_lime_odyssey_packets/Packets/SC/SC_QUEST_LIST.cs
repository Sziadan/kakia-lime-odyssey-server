using kakia_lime_odyssey_packets.Packets.Enums;
using kakia_lime_odyssey_packets.Packets.Interface;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

public struct SC_QUEST_LIST : IPacketVar
{
	public int completedMain;
	public int completedSub;
	public int completedNormal;
	public List<QuestDetails> details;
}

public struct QuestDetails
{
	public uint questId;
	public QUEST_STATE questState;
	public string questDescription;
}