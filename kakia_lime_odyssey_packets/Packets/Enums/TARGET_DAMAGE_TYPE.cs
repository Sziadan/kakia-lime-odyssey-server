﻿namespace kakia_lime_odyssey_packets.Packets.Enums;

public enum TARGET_DAMAGE_TYPE : int
{
	TDT_ATTACKED_NORMAL = 0x0,
	TDT_ATTACKED_NORMAL_CRITICAL = 0x1,
	TDT_ATTACKED_BY_SKILL = 0x2,
	TDT_ATTACKED_BY_SKILL_CRITICAL = 0x3,
	TDT_ATTACKED_BY_STREAM = 0x4,
	TDT_HEALED = 0x5,
	TDT_HEALED_CRITICAL = 0x6
};
