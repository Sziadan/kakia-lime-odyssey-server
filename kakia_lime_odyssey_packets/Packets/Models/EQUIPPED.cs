using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

/*
[StructLayout(LayoutKind.Sequential)]
public struct EQUIPPED
{
	public int neck;
	public int weapon;
	public int weaponGuard;
	public int range;
	public int spending;
	public int helm;
	public int band;
	public int eye;
	public int mouth;
	public int shoulder;
	public int upper;
	public int hand;
	public int belt;
	public int under;
	public int foot;
	public int relic;
	public int ring1;
	public int ring2;
	public int ear;
	public int accessory;
}
*/

[StructLayout(LayoutKind.Sequential)]
public struct EQUIPPED
{
	public int NONE;
	public int MAIN_EQUIP;
	public int SUB_EQUIP;
	public int RANGE_MAIN_EQUIP;
	public int SPENDING;
	public int HEAD;
	public int FOREHEAD;
	public int EYE;
	public int MOUTH;
	public int NECK;
	public int SHOULDER;
	public int UPPER_BODY;
	public int HAND;
	public int WAIST;
	public int LOWER_BODY;
	public int FOOT;
	public int RELIC;
	public int RING_1;
	public int RING_2;
	public int ACCESSORY_1;
}


public class ModEquipped
{
	public int NONE;
	public int MAIN_EQUIP;
	public int SUB_EQUIP;
	public int RANGE_MAIN_EQUIP;
	public int SPENDING;
	public int HEAD;
	public int FOREHEAD;
	public int EYE;
	public int MOUTH;
	public int NECK;
	public int SHOULDER;
	public int UPPER_BODY;
	public int HAND;
	public int WAIST;
	public int LOWER_BODY;
	public int FOOT;
	public int RELIC;
	public int RING_1;
	public int RING_2;
	public int ACCESSORY_1;
	public int ACCESSORY_2;

	public ModEquipped(EQUIPPED other)
	{
		this.NONE = other.NONE;
		this.MAIN_EQUIP = other.MAIN_EQUIP;
		this.SUB_EQUIP = other.SUB_EQUIP;
		this.RANGE_MAIN_EQUIP = other.RANGE_MAIN_EQUIP;
		this.SPENDING = other.SPENDING;
		this.HEAD = other.HEAD;
		this.FOREHEAD = other.FOREHEAD;
		this.EYE = other.EYE;
		this.MOUTH = other.MOUTH;
		this.NECK = other.NECK;
		this.SHOULDER = other.SHOULDER;
		this.UPPER_BODY = other.UPPER_BODY;
		this.HAND = other.HAND;
		this.WAIST = other.WAIST;
		this.LOWER_BODY = other.LOWER_BODY;
		this.FOOT = other.FOOT;
		this.RELIC = other.RELIC;
		this.RING_1 = other.RING_1;
		this.RING_2 = other.RING_2;
		this.ACCESSORY_1 = other.ACCESSORY_1;
	}

	public EQUIPPED AsStruct()
	{
		return new EQUIPPED
		{
			NONE = this.NONE,
			MAIN_EQUIP = this.MAIN_EQUIP,
			SUB_EQUIP = this.SUB_EQUIP,
			RANGE_MAIN_EQUIP = this.RANGE_MAIN_EQUIP,
			SPENDING = this.SPENDING,
			HEAD = this.HEAD,
			FOREHEAD = this.FOREHEAD,
			EYE = this.EYE,
			MOUTH = this.MOUTH,
			NECK = this.NECK,
			SHOULDER = this.SHOULDER,
			UPPER_BODY = this.UPPER_BODY,
			HAND = this.HAND,
			WAIST = this.WAIST,
			LOWER_BODY = this.LOWER_BODY,
			FOOT = this.FOOT,
			RELIC = this.RELIC,
			RING_1 = this.RING_1,
			RING_2 = this.RING_2,
			ACCESSORY_1 = this.ACCESSORY_1
		};
	}
}