namespace kakia_lime_odyssey_packets.Packets.Enums;

public enum MOVE_TYPE : int
{
  MOVE_TYPE_STOP = 0x0,
  MOVE_TYPE_RUN = 0x1,
  MOVE_TYPE_WALK = 0x2,
  MOVE_TYPE_SWIM = 0x3,
  MOVE_TYPE_FLY = 0x4,
  MOVE_TYPE_KNOCK_BACK = 0x5,
  MOVE_TYPE_KNOCK_FLOWN = 0x6,
  MOVE_TYPE_STAND_UP_FROM_KNOCK = 0x7,
  MOVE_TYPE_RELEASE_FROM_KNOCK = 0x8
};
