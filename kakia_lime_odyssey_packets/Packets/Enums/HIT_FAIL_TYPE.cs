namespace kakia_lime_odyssey_packets.Packets.Enums;

public enum HIT_FAIL_TYPE : int
{
	HIT_FAIL_NONE = 0x0,
	HIT_FAIL_HIT = 0x1,
	HIT_FAIL_CRITICAL_HIT = 0x2,
	HIT_FAIL_MISS = 0x3,
	HIT_FAIL_AVOID = 0x4,
	HIT_FAIL_SHIELD = 0x5,
	HIT_FAIL_GUARD = 0x6
};
