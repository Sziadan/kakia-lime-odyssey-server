namespace kakia_lime_odyssey_packets.Packets.Enums;

public enum QUEST_STATE : byte
{
	QUEST_STATE_COMPLETED = 0x1,
	QUEST_STATE_PROGRESSING = 0x2,
	QUEST_STATE_FAILED = 0x3,
	QUEST_STATE_GIVENUP = 0x4,
	QUEST_STATE_NONE = 0x5
};
