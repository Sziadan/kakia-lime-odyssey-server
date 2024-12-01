﻿namespace kakia_lime_odyssey_packets;

public enum PacketType_REV345 : UInt16
{
	SYSTEM_FIRST = 0x5,
	SC_EXCEPTION = 0x6,
	SC_TYPICAL_EXCEPTION = 0x7,
	SC_WAITING_QUEUE_STATE = 0x8,
	CS_NOTICE = 0x9,
	SC_NOTICE = 0xA,
	CS_DOWNLOAD_FILE = 0xB,
	SC_START_DOWNLOAD_FILE = 0xC,
	SC_DOWNLOAD_FILE_BLOCK = 0xD,
	CS_DOWNLOAD_NEXT_FILE_BLOCK = 0xE,
	CS_UPLOAD_FILE = 0xF,
	SC_START_UPLOAD_FILE = 0x10,
	CS_UPLOAD_FILE_BLOCK = 0x11,
	SC_UPLOAD_NEXT_FILE_BLOCK = 0x12,
	SYSTEM_LAST = 0x13,

	LOBBY_FIRST = 0x32,
	SC_ENTER_LOBBY = 0x33,
	SC_REENTER_LOBBY = 0x34,
	CS_LOGIN = 0x35,
	SC_LOGIN_RESULT = 0x36,
	CS_CHANNEL_SELECT = 0x37,
	SC_CHANNEL_SELECT = 0x38,
	SC_PC_LIST = 0x39,
	CS_CREATE_PC = 0x3A,
	SC_CREATED_PC = 0x3B,
	CS_DELETE_PC = 0x3C,
	SC_DELETED_PC = 0x3D,
	CS_START_GAME = 0x3E,
	SC_FILE_LIST = 0x3F,
	LOBBY_LAST = 0x40,

	ZONE_FIRST = 0x64,
	SC_READY = 0x65,
	CS_READY = 0x66,
	SC_ENTER_ZONE = 0x67,
	SC_TICK = 0x68,
	SC_ZONE_TRANSFERING = 0x69,
	CS_PING = 0x6A,
	SC_PONG = 0x6B,
	SC_CREATING_MISSION_ZONE = 0x6C,
	CS_CANCEL_CREATE_MISSION_ZONE = 0x6D,
	CS_RETURN_LOBBY = 0x6E,
	SC_RETURN_LOBBY_FAILED = 0x6F,
	SC_TIME = 0x70,
	SC_NEW_POST_ALARM = 0x71,
	SC_POST_LIST = 0x72,
	CS_REQUEST_POST = 0x73,
	SC_POST = 0x74,
	CS_TAKE_POST_ITEM = 0x75,
	SC_POST_ITEM = 0x76,
	CS_REQUEST_DELETE_POST = 0x77,
	SC_DELETED_POST = 0x78,
	CS_SEND_POST = 0x79,
	SC_SEND_POST_RESULT = 0x7A,
	SC_CANCELED_CREATE_MISSION_ZONE = 0x7B,
	ZONE_LAST = 0x7C,

	BEHAVIORAL_FIRST = 0x96,
	CS_MOVE_PC = 0x97,
	CS_SLIDE_PC = 0x98,
	CS_STOP_PC = 0x99,
	CS_DIRECTION_PC = 0x9A,
	SC_DIRECTION = 0x9B,
	CS_JUMP_PC = 0x9C,
	CS_SAY_PC = 0x9D,
	CS_PARTY_SAY_PC = 0x9E,
	CS_GUILD_SAY_PC = 0x9F,
	CS_KNIGHTS_SAY_PC = 0xA0,
	CS_REALM_SAY_PC = 0xA1,
	CS_WHISPER_PC = 0xA2,
	CS_SHOUT_PC = 0xA3,
	CS_SIT_DOWN_PC = 0xA4,
	CS_STAND_UP_PC = 0xA5,
	SC_MOVE = 0xA6,
	SC_MOVE_TO_TARGET = 0xA7,
	SC_SLIDE_PC = 0xA8,
	SC_STOP = 0xA9,
	SC_JUMP_PC = 0xAA,
	SC_SAY = 0xAB,
	SC_PARTY_SAY = 0xAC,
	SC_GUILD_SAY = 0xAD,
	SC_KNIGHTS_SAY = 0xAE,
	SC_REALM_SAY = 0xAF,
	SC_WHISPER = 0xB0,
	SC_SHOUT = 0xB1,
	SC_SIT_DOWN = 0xB2,
	SC_STAND_UP = 0xB3,
	CS_SELECT_ACTION_TARGET = 0xB4,
	SC_SELECT_ACTION_TARGET = 0xB5,
	CS_CANCEL_SELECTED_ACTION_TARGET = 0xB6,
	SC_CANCEL_SELECTED_ACTION_TARGET = 0xB7,
	SC_DEAD = 0xB8,
	CS_RESURRECT = 0xB9,
	SC_RESURRECTED = 0xBA,
	SC_RIDE_PC = 0xBB,
	CS_UNRIDE_PC = 0xBC,
	SC_UNRIDE_PC = 0xBD,
	CS_CHANGE_JOB_CLASS = 0xBE,
	SC_CHANGED_JOB_CLASS = 0xBF,
	SC_INSERT_DEF = 0xC0,
	SC_REMOVE_DEF = 0xC1,
	SC_DEF_LIST = 0xC2,
	SC_RELEASE_KNOCK = 0xC3,
	SC_KNOCK_PUSHED = 0xC4,
	SC_KNOCK_FLOWN = 0xC5,
	CS_CHANGE_HELM_SHOWMODE = 0xC6,
	SC_CHANGE_HELM_SHOWMODE = 0xC7,
	CS_FELL_PC = 0xC8,
	CS_ESCAPE = 0xC9,
	CS_FINISH_WARP = 0xCA,
	BEHAVIORAL_LAST = 0xCB,

	SIGHT_FIRST = 0xFA,
	SC_ENTER_SIGHT_PC = 0xFB,
	SC_LEAVE_SIGHT_PC = 0xFC,
	SC_ENTER_SIGHT_MONSTER = 0xFD,
	SC_LEAVE_SIGHT_MONSTER = 0xFE,
	SC_ENTER_SIGHT_VILLAGER = 0xFF,
	SC_LEAVE_SIGHT_VILLAGER = 0x100,
	SC_ENTER_SIGHT_QUEST_BOARD = 0x101,
	SC_LEAVE_SIGHT_QUEST_BOARD = 0x102,
	SC_ENTER_SIGHT_PROP = 0x103,
	SC_LEAVE_SIGHT_PROP = 0x104,
	SC_ENTER_SIGHT_MERCHANT = 0x105,
	SC_LEAVE_SIGHT_MERCHANT = 0x106,
	SC_ENTER_SIGHT_TRANSFER = 0x107,
	SC_LEAVE_SIGHT_TRANSFER = 0x108,
	SC_ENTER_SIGHT_HOUSE = 0x109,
	SC_LEAVE_SIGHT_HOUSE = 0x10A,
	SC_ENTER_SIGHT_BULLET_SKILL = 0x10B,
	SC_LEAVE_SIGHT_BULLET_SKILL = 0x10C,
	SC_ENTER_SIGHT_BULLET_ITEM = 0x10D,
	SC_LEAVE_SIGHT_BULLET_ITEM = 0x10E,
	SC_LEAVE_ZONEOBJ = 0x10F,
	SIGHT_LAST = 0x110,

	COMBAT_FIRST = 0x12C,
	CS_SELECT_TARGET_READY_WEAPON_HITING = 0x12D,
	CS_READY_WEAPON_HITING = 0x12E,
	CS_CANCEL_READY_WEAPON_HITING = 0x12F,
	SC_START_COMBATING = 0x130,
	SC_STOP_COMBATING = 0x131,
	SC_WEAPON_HIT_RESULT = 0x132,
	SC_RELEASE_COMBO = 0x133,
	CS_USE_STREAM_GAUGE = 0x134,
	SC_START_USABLE_STREAM = 0x135,
	SC_FINISH_USABLE_STREAM = 0x136,
	SC_START_USING_STREAM = 0x137,
	SC_FINISH_USING_STREAM = 0x138,
	SC_START_RESTING_STREAM = 0x139,
	SC_FINISH_RESTING_STREAM = 0x13A,
	COMBAT_LAST = 0x13B,

	DIALOG_FIRST = 0x15E,
	CS_SELECT_AND_REQUEST_TALKING = 0x15F,
	CS_REQUEST_TALKING = 0x160,
	SC_TALKING = 0x161,
	CS_TALKING_CANCEL = 0x162,
	CS_TALKING_CONFIRM = 0x163,
	SC_TALKING_CHOICE = 0x164,
	CS_TALKING_CHOICE_CLICK = 0x165,
	SC_FINISH_TALKING = 0x166,
	DIALOG_LAST = 0x167,

	QUEST_FIRST = 0x190,
	CS_SELECT_AND_REQUEST_BOARD_QUESTS = 0x191,
	CS_REQUEST_BOARD_QUESTS = 0x192,
	SC_BOARD_QUESTS = 0x193,
	CS_BOARD_CANCEL = 0x194,
	SC_BOARD_FINISH = 0x195,
	SC_QUEST_LIST = 0x196,
	CS_QUEST_ADD = 0x197,
	SC_QUEST_ADD = 0x198,
	CS_QUEST_DELETE = 0x199,
	SC_QUEST_DELETE = 0x19A,
	SC_UPDATE_QUEST_STATE = 0x19B,
	SC_CHANGE_QUEST_SUBJECT = 0x19C,
	SC_QUEST_FAIL = 0x19D,
	SC_QUEST_REPORT_TALK = 0x19E,
	CS_QUEST_COMPLETE = 0x19F,
	SC_QUEST_COMPLETE = 0x1A0,
	CS_QUEST_LIST = 0x1A1,
	QUEST_LAST = 0x1A2,

	STATUS_FIRST = 0x1C2,
	SC_COMMON_STATUS = 0x1C3,
	CS_REQUEST_COMMON_STATUS = 0x1C4,
	SC_PC_STATUS = 0x1C5,
	CS_REQUEST_PC_STATUS = 0x1C6,
	SC_PC_NATURAL_RECOVERY_HP = 0x1C7,
	SC_PC_NATURAL_RECOVERY_MP = 0x1C8,
	SC_PC_NATURAL_RECOVERY_LP = 0x1C9,
	SC_PC_LIFE_JOB_LEVEL_UP = 0x1CA,
	SC_GOT_LIFE_JOB_EXP = 0x1CB,
	CS_DISTRIBUTE_LIFE_JOB_STATUS_POINT = 0x1CC,
	SC_PC_COMBAT_JOB_LEVEL_UP = 0x1CD,
	SC_GOT_COMBAT_JOB_EXP = 0x1CE,
	SC_CHANGED_STREAM_GAUGE = 0x1CF,
	SC_CHANGED_HP = 0x1D0,
	SC_CHANGED_MP = 0x1D1,
	SC_CHANGED_LP = 0x1D2,
	SC_CHANGED_MHP = 0x1D3,
	SC_CHANGED_MMP = 0x1D4,
	SC_CHANGED_MLP = 0x1D5,
	SC_CHANGED_STR = 0x1D6,
	SC_CHANGED_INL = 0x1D7,
	SC_CHANGED_DEX = 0x1D8,
	SC_CHANGED_AGI = 0x1D9,
	SC_CHANGED_VIT = 0x1DA,
	SC_CHANGED_SPI = 0x1DB,
	SC_CHANGED_IDE = 0x1DC,
	SC_CHANGED_SES = 0x1DD,
	SC_CHANGED_CRT = 0x1DE,
	SC_CHANGED_MELEE_HIT_RATE = 0x1DF,
	SC_CHANGED_DODGE = 0x1E0,
	SC_CHANGED_CRITICAL_RATE = 0x1E1,
	SC_CHANGED_MELEE_ATK = 0x1E2,
	SC_CHANGED_MELEE_DEFENSE = 0x1E3,
	SC_CHANGED_SPELL_ATK = 0x1E4,
	SC_CHANGED_SPELL_DEFENSE = 0x1E5,
	SC_CHANGED_PARRY = 0x1E6,
	SC_CHANGED_BLOCK = 0x1E7,
	SC_CHANGED_HIT_SPEED_RATIO = 0x1E8,
	SC_DISTRIBUTED_LIFE_JOB_STATUS_POINT = 0x1E9,
	SC_CHANGED_MOVABLE = 0x1EA,
	SC_CHANGED_WEAPON_HITABLE = 0x1EB,
	SC_CHANGED_SKILL_USABLE = 0x1EC,
	SC_CHANGED_ITEM_USABLE = 0x1ED,
	SC_CHANGED_ADJUST_SKILL_CASTING = 0x1EE,
	SC_CHANGED_ADJUST_ITEM_CASTING = 0x1EF,
	SC_CHANGED_LIFE_JOB_SKILL_POINT = 0x1F0,
	SC_CHANGED_ADJUST_SKILL_CASTING_RATIO = 0x1F1,
	SC_CHANGED_ADJUST_ITEM_CASTING_RATIO = 0x1F2,
	SC_CHANGED_ADJUST_SKILL_APPLYING_RANGE = 0x1F3,
	SC_CHANGED_ADJUST_ITEM_APPLYING_RANGE = 0x1F4,
	SC_CHANGED_ADJUST_SKILL_USE_MP = 0x1F5,
	SC_CHANGED_VELOCITIES = 0x1F6,
	SC_CHANGED_VELOCITY_RATIO = 0x1F7,
	SC_START_HOLDING_BREATH = 0x1F8,
	SC_FINISH_HOLDING_BREATH = 0x1F9,
	SC_CHANGED_COLLECT_SUCESS_RATE = 0x1FA,
	SC_CHANGED_COLLECTION_INCREASE_RATE = 0x1FB,
	SC_CHANGED_MAKE_TIME_DECREASE_AMOUNT = 0x1FC,
	SC_CHANGED_COMBAT_STATUS = 0x1FD,
	SC_CHANGED_LIFE_STATUS = 0x1FE,
	CS_HEAD_UNDER_WATER = 0x1FF,
	CS_HEAD_OVER_WATER = 0x200,
	STATUS_LAST = 0x201,

	MAKING_FIRST = 0x226,
	CS_ITEM_MAKE_READY = 0x227,
	CS_ITEM_MAKE_REQUEST_REPORT = 0x228,
	SC_ITEM_MAKE_UPDATE_REPORT = 0x229,
	CS_ITEM_MAKE_START = 0x22A,
	SC_ITEM_MAKE_START_CASTING = 0x22B,
	SC_ITEM_MAKE_FINISH = 0x22C,
	CS_ITEM_MAKE_CANCEL = 0x22D,
	CS_ITEM_MAKE_CONTINUALLY = 0x22E,
	CS_ITEM_MAKE_CONTINUALLY_STOP = 0x22F,
	CS_STUFF_MAKE_READY = 0x230,
	SC_STUFF_MAKE_READY_SUCCESS = 0x231,
	CS_STUFF_MAKE_ADD_LIST = 0x232,
	SC_STUFF_MAKE_ADD_LIST_SUCCESS = 0x233,
	CS_STUFF_MAKE_DELETE_LIST = 0x234,
	SC_STUFF_MAKE_DELETE_LIST_SUCCESS = 0x235,
	CS_STUFF_MAKE_START = 0x236,
	SC_STUFF_MAKE_START_CASTING = 0x237,
	SC_STUFF_MAKE_FINISH = 0x238,
	CS_STUFF_MAKE_CANCEL = 0x239,
	MAKING_LAST = 0x23A,

	INVENTORY_FIRST = 0x258,
	SC_INVENTORY_ITEM_LIST = 0x259,
	SC_BANK_OPENED = 0x25A,
	SC_PET_ITEM_LIST = 0x25B,
	SC_INSERT_SLOT_ITEM = 0x25C,
	SC_UPDATE_SLOT_ITEM = 0x25D,
	SC_DELETE_SLOT_ITEM = 0x25E,
	CS_MOVE_SLOT_ITEM = 0x25F,
	SC_MOVE_SLOT_ITEM = 0x260,
	CS_DISCARD_SLOT_ITEM = 0x261,
	CS_USE_INVENTORY_ITEM_POS = 0x262,
	CS_USE_INVENTORY_ITEM_OBJ = 0x263,
	CS_USE_INVENTORY_ITEM_ACTION_TARGET = 0x264,
	CS_USE_INVENTORY_ITEM_SELF = 0x265,
	CS_USE_INVENTORY_ITEM_SLOT = 0x266,
	CS_CANCEL_USING_ITEM = 0x267,
	SC_CANCELED_USING_ITEM = 0x268,
	SC_START_CASTING_ITEM_OBJ = 0x269,
	SC_START_CASTING_ITEM_POS = 0x26A,
	SC_START_CASTING_ITEM_SLOT = 0x26B,
	SC_DELAYED_CASTING_ITEM = 0x26C,
	SC_LAUNCH_BULLET_ITEM_OBJ = 0x26D,
	SC_LAUNCH_BULLET_ITEM_POS = 0x26E,
	SC_LAUNCHED_BULLET_ITEM_RESULT_LIST = 0x26F,
	SC_TERMINATED_CONTINUOUS_BULLETITEM = 0x270,
	SC_USE_ITEM_OBJ_RESULT_LIST = 0x271,
	SC_USE_ITEM_POS_RESULT_LIST = 0x272,
	SC_USE_ITEM_SLOT_RESULT = 0x273,
	CS_READY_INVENTORY_COMPOSE_ITEM = 0x274,
	SC_READY_INVENTORY_COMPOSE_ITEM = 0x275,
	CS_CANCEL_INVENTORY_COMPOSE_ITEM = 0x276,
	CS_USE_INVENTORY_COMPOSE_ITEM_SLOTS = 0x277,
	SC_INVENTORY_COMPOSE_ITEM_FINISH = 0x278,
	CS_SLOT_ITEM_INFO = 0x279,
	SC_SLOT_ITEM_INFO = 0x27A,
	SC_UPDATE_ITEM_SLOT_COUNT = 0x27B,
	SC_UPDATE_DURABILITY_INVENTORY_ITEM = 0x27C,
	INVENTORY_LAST = 0x27D,

	EQUIPMENT_FIRST = 0x28A,
	SC_COMBAT_JOB_EQUIP_ITEM_LIST = 0x28B,
	CS_COMBAT_JOB_EQUIP_ITEM = 0x28C,
	SC_COMBAT_JOB_EQUIPMENT_LIST = 0x28D,
	CS_COMBAT_JOB_UNEQUIP_ITEM = 0x28E,
	SC_COMBAT_JOB_UNEQUIP_ITEM = 0x28F,
	SC_COMBAT_JOB_WIRED_UNEQUIP_ITEM = 0x290,
	SC_COMBAT_JOB_UPDATE_DURABILITY_EQUIPED_ITEM = 0x291,
	SC_LIFE_JOB_EQUIP_ITEM_LIST = 0x292,
	CS_LIFE_JOB_EQUIP_ITEM = 0x293,
	SC_LIFE_JOB_EQUIPMENT_LIST = 0x294,
	CS_LIFE_JOB_UNEQUIP_ITEM = 0x295,
	SC_LIFE_JOB_UNEQUIP_ITEM = 0x296,
	SC_LIFE_JOB_WIRED_UNEQUIP_ITEM = 0x297,
	SC_LIFE_JOB_UPDATE_DURABILITY_EQUIPED_ITEM = 0x298,
	CS_EQUIP_ITEM_INFO = 0x299,
	SC_EQUIP_ITEM_INFO = 0x29A,
	CS_ITEM_REPAIR_PRICE = 0x29B,
	SC_ITEM_REPAIR_PRICE = 0x29C,
	CS_EQUIPED_ITEM_REPAIR_PRICE = 0x29D,
	SC_EQUIPED_ITEM_REPAIR_PRICE = 0x29E,
	CS_ITEM_REPAIR_REQUEST = 0x29F,
	SC_ITEM_REPAIR_RESULT = 0x2A0,
	CS_EQUIPED_ITEM_REPAIR_REQUEST = 0x2A1,
	SC_EQUIPED_ITEM_REPAIR_RESULT = 0x2A2,
	EQUIPMENT_LAST = 0x2A3,

	LOOTING_FIRST = 0x2BC,
	CS_SELECT_TARGET_REQUEST_START_LOOTING = 0x2BD,
	CS_REQUEST_START_LOOTING = 0x2BE,
	SC_LOOTABLE_ITEM_LIST = 0x2BF,
	CS_LOOT_ITEM = 0x2C0,
	SC_START_LOOTING = 0x2C1,
	SC_FINISH_LOOTING = 0x2C2,
	CS_FINISH_LOOTING = 0x2C3,
	SC_LOOTABLE = 0x2C4,
	SC_LOOTABLE_PRIVATE_ITEM = 0x2C5,
	SC_DISABLED_LOOTING = 0x2C6,
	SC_DISABLED_LOOTING_PRIVATE_ITEM = 0x2C7,
	SC_LOOTED_ITEM = 0x2C8,
	LOOTING_LAST = 0x2C9,

	SKILL_FIRST = 0x2EE,
	SC_SKILL_LIST = 0x2EF,
	SC_SKILL_ADD = 0x2F0,
	CS_USE_SKILL_ACTION_TARGET = 0x2F1,
	CS_USE_SKILL_OBJ = 0x2F2,
	CS_USE_SKILL_SELF = 0x2F3,
	CS_USE_SKILL_POS = 0x2F4,
	CS_CANCEL_USING_SKILL = 0x2F5,
	SC_CANCELED_USING_SKILL = 0x2F6,
	SC_START_CASTING_SKILL_OBJ = 0x2F7,
	SC_START_CASTING_SKILL_POS = 0x2F8,
	SC_DELAYED_CASTING_SKILL = 0x2F9,
	SC_LAUNCH_BULLET_SKILL_OBJ = 0x2FA,
	SC_LAUNCH_BULLET_SKILL_POS = 0x2FB,
	SC_LAUNCHED_BULLET_SKILL_RESULT_LIST = 0x2FC,
	SC_TERMINATED_CONTINUOUS_BULLETSKILL = 0x2FD,
	SC_USE_SKILL_OBJ_RESULT_LIST = 0x2FE,
	SC_USE_SKILL_POS_RESULT_LIST = 0x2FF,
	SC_SKILL_LV_UP = 0x300,
	SC_SKILL_DEL = 0x301,
	SC_ENABLE_USING_SKILL = 0x302,
	SC_DISABLE_USING_SKILL = 0x303,
	SC_LEARNABLE_SKILL_LIST = 0x304,
	CS_REQUEST_LEARN_SKILL = 0x305,
	SKILL_LAST = 0x306,

	EXCHANGE_FIRST = 0x320,
	CS_SELECT_TARGET_REQUEST_EXCHANGE = 0x321,
	CS_REQUEST_EXCHANGE = 0x322,
	SC_REQUEST_EXCHANGE = 0x323,
	SC_EXCHANGE_REQUESTED = 0x324,
	CS_EXCHANGE_ACCEPT = 0x325,
	CS_EXCHANGE_REJECT = 0x326,
	SC_START_EXCHANGE = 0x327,
	CS_ADD_ITEM_TO_EXCHANGE_LIST = 0x328,
	SC_SUCCESS_ADD_ITEM_TO_EXCHANGE_LIST = 0x329,
	SC_ADDED_ITEM_TO_EXCHANGE_LIST = 0x32A,
	CS_SUBTRACT_ITEM_FROM_EXCHANGE_LIST = 0x32B,
	SC_SUCCESS_SUBTRACT_ITEM_FROM_EXCHANGE_LIST = 0x32C,
	SC_SUBTRACTED_ITEM_FROM_EXCHANGE_LIST = 0x32D,
	CS_EXCHANGE_READY = 0x32E,
	SC_EXCHANGE_READY = 0x32F,
	SC_EXCHANGE_READY_ALL = 0x330,
	CS_EXCHANGE_AGAIN = 0x331,
	SC_EXCHANGE_AGAIN = 0x332,
	CS_EXCHANGE_OK = 0x333,
	SC_EXCHANGE_OK = 0x334,
	CS_EXCHANGE_CANCEL = 0x335,
	SC_EXCHANGE_SUCCESS = 0x336,
	SC_EXCHANGE_FAIL = 0x337,
	EXCHANGE_LAST = 0x338,

	TRADE_FIRST = 0x352,
	CS_SELECT_TARGET_REQUEST_TRADE = 0x353,
	CS_REQUEST_TRADE = 0x354,
	SC_TRADE_DESC = 0x355,
	CS_REQUEST_SOLD_ITEMS = 0x356,
	SC_SOLD_ITEM_LIST = 0x357,
	CS_TRADE_BUY = 0x358,
	CS_TRADE_BUY_SOLD_ITEMS = 0x359,
	SC_TRADE_BOUGHT_SOLD_ITEMS = 0x35A,
	CS_REQUEST_TRADE_PRICE = 0x35B,
	SC_TRADE_PRICE = 0x35C,
	CS_TRADE_SELL = 0x35D,
	CS_TRADE_END = 0x35E,
	SC_TRADE_END = 0x35F,
	TRADE_LAST = 0x360,

	PARTY_FIRST = 0x384,
	CS_PARTY_CREATE = 0x385,
	SC_PARTY_CREATED = 0x386,
	CS_PARTY_DISBAND = 0x387,
	SC_PARTY_DISBANDED = 0x388,
	CS_PARTY_JOIN = 0x389,
	SC_PARTY_JOINED = 0x38A,
	SC_PARTY_MEMBER_ADDED = 0x38B,
	CS_PARTY_SECEDE = 0x38C,
	SC_PARTY_SECEDED = 0x38D,
	CS_PARTY_INVITE = 0x38E,
	SC_PARTY_INVITED = 0x38F,
	CS_PARTY_REFUSE_INVITE = 0x390,
	CS_PARTY_REQUEST_JOIN = 0x391,
	CS_PARTY_BAN = 0x392,
	SC_PARTY_MEMBER_BANNED = 0x393,
	SC_PARTY_MEMBER_CONNECTED = 0x394,
	SC_PARTY_MEMBER_DISCONNECTED = 0x395,
	CS_PARTY_CHANGE_LEADER = 0x396,
	SC_PARTY_CHANGED_LEADER = 0x397,
	CS_PARTY_CHANGE_OPTION = 0x398,
	SC_PARTY_CHANGED_OPTION = 0x399,
	SC_PARTY_MEMBER_STATE = 0x39A,
	SC_PARTY_MEMBER_NAME = 0x39B,
	SC_PARTY_UPDATE_MEMBER_LV = 0x39C,
	SC_PARTY_UPDATE_MEMBER_COMBAT_JOB_LV = 0x39D,
	SC_PARTY_UPDATE_MEMBER_LIFE_JOB_LV = 0x39E,
	SC_PARTY_UPDATE_MEMBER_HP = 0x39F,
	SC_PARTY_UPDATE_MEMBER_MP = 0x3A0,
	SC_PARTY_UPDATE_MEMBER_LP = 0x3A1,
	SC_PARTY_UPDATE_MEMBER_POS = 0x3A2,
	SC_PARTY_CHANGED_MEMBER_PLAYING_JOB = 0x3A3,
	SC_PARTY_MEMBER_ADD_DEF = 0x3A4,
	SC_PARTY_MEMBER_DEL_DEF = 0x3A5,
	SC_PARTY_MEMBER_LOOTED_ITEM = 0x3A6,
	PARTY_LAST = 0x3A7,

	GUILD_FIRST = 0x3B6,
	SC_GUILD_CREATE_READY = 0x3B7,
	CS_GUILD_CREATE = 0x3B8,
	SC_GUILD_CREATED = 0x3B9,
	CS_GUILD_DISBAND = 0x3BA,
	SC_GUILD_DISBANDED = 0x3BB,
	CS_GUILD_JOIN = 0x3BC,
	SC_GUILD_INFO = 0x3BD,
	SC_GUILD_MEMBER_ADDED = 0x3BE,
	CS_GUILD_SECEDE = 0x3BF,
	SC_GUILD_SECEDED = 0x3C0,
	CS_GUILD_INVITE = 0x3C1,
	SC_GUILD_INVITED = 0x3C2,
	CS_GUILD_REFUSE_INVITE = 0x3C3,
	CS_GUILD_REQUEST_JOIN = 0x3C4,
	CS_GUILD_BAN = 0x3C5,
	SC_GUILD_MEMBER_CONNECTED = 0x3C6,
	SC_GUILD_MEMBER_DISCONNECTED = 0x3C7,
	CS_GUILD_NOTICE = 0x3C8,
	SC_GUILD_NOTICE = 0x3C9,
	CS_GUILD_CHANGE_LEADER = 0x3CA,
	SC_GUILD_CHANGED_LEADER = 0x3CB,
	CS_GUILD_CHANGE_OPTION = 0x3CC,
	SC_GUILD_CHANGED_OPTION = 0x3CD,
	SC_GUILD_MEMBER_STATE = 0x3CE,
	SC_GUILD_UPDATE_MEMBER_COMBAT_JOB_LV = 0x3CF,
	SC_GUILD_UPDATE_MEMBER_LIFE_JOB_LV = 0x3D0,
	SC_GUILD_NAME_UPDATE_SIGHT = 0x3D1,
	GUILD_LAST = 0x3D2,

	PRIVATE_CHAT_ROOM_FIRST = 0x3E8,
	CS_PRIVATE_CHATROOM_CREATE = 0x3E9,
	CS_PRIVATE_CHATROOM_DESTROY = 0x3EA,
	SC_PRIVATE_CHATROOM_DESTROYED = 0x3EB,
	CS_PRIVATE_CHATROOM_ENTER = 0x3EC,
	SC_PRIVATE_CHATROOM_ENTERED = 0x3ED,
	SC_PRIVATE_CHATROOM_MEMBER_ADDED = 0x3EE,
	CS_PRIVATE_CHATROOM_BAN = 0x3EF,
	CS_PRIVATE_CHATROOM_LEAVE = 0x3F0,
	SC_PRIVATE_CHATROOM_LEAVED = 0x3F1,
	CS_PRIVATE_CHATROOM_SAY = 0x3F2,
	SC_PRIVATE_CHATROOM_SAY = 0x3F3,
	PRIVATE_CHAT_ROOM_LAST = 0x3F4,

	ETC_FIRST = 0x41A,
	SC_DO_ACTION = 0x41B,
	SC_UPDATED_APPEARANCE_PC = 0x41C,
	SC_DO_EXPRESSION = 0x41D,
	SC_DO_RELATION = 0x41E,
	SC_CHANGE_TO_PC_SHAPE = 0x41F,
	SC_CHANGE_TO_NPC_SHAPE = 0x420,
	SC_CHANGE_SCALE = 0x421,
	SC_TRANSPARENT = 0x422,
	SC_COLOR = 0x423,
	SC_CALL_EFFECT = 0x424,
	SC_CALL_EFFECT_POS = 0x425,
	SC_CALL_SOUND = 0x426,
	SC_WARP = 0x427,
	SC_CHOICE_COMBAT_JOB = 0x428,
	CS_CHOICED_COMBAT_JOB = 0x429,
	SC_SELECTED_COMBAT_JOB = 0x42A,
	SC_CHOICE_LIFE_JOB = 0x42B,
	CS_CHOICED_LIFE_JOB = 0x42C,
	SC_SELECTED_LIFE_JOB = 0x42D,
	SC_START_CONTINUOUS_ACTION = 0x42E,
	SC_FINISH_CONTINUOUS_ACTION = 0x42F,
	SC_MACRO_CAMERA = 0x430,
	SC_PRINT_IMAGE = 0x431,
	ETC_LAST = 0x432,

	UNKNOWN
}
