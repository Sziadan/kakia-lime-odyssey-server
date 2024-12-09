using kakia_lime_odyssey_network.Interface;
using kakia_lime_odyssey_packets.Packets.Enums;
using kakia_lime_odyssey_packets.Packets.Models;
using System.Xml.Serialization;

namespace kakia_lime_odyssey_server.Models;

[XmlRoot]
public class ItemInfo
{
	[XmlElement("Item")]
	public List<Item> Items { get; set; }

	public static List<Item> GetItems()
	{
		XmlSerializer serializer = new XmlSerializer(typeof(ItemInfo));
		using FileStream fileStream = new FileStream("db/xmls/ItemInfo.xml", FileMode.Open);
		var info = (ItemInfo)serializer.Deserialize(fileStream)!;

		return info.Items;
	}
}

public class LootableItem
{
	public int Id { get; set; }
	public double DropRate { get; set; }
}

public enum ItemType
{
	None = 0,
	MainEquipment = 1,
	AuxiliaryEquipment = 2,
	RemoteEquipment = 3,
	ConsumableEquipment = 4,
	Head = 5,
	Forehead = 6,
	Eyes = 7,
	Mouth = 8,
	Neck = 9,
	Shoulder = 10,
	Top = 11,
	Hand = 12,
	Waist = 13,
	Bottom = 14,
	Feet = 15,
	Artifact = 16,
	Ring1 = 17,
	Ring2 = 18,
	Accessory1 = 19,
	Accessory2 = 20,
	Bag1 = 21,
	Bag2 = 22,
	Bag3 = 23,
	Bag4 = 24,
	Bag5 = 25,
	Consumables = 97,
	Collectibles = 98,
	Quests = 99,
	Artifacts = 101,
	Others = 102
}

public enum WeaponType
{
	BareHand = 0,
	Dagger = 1,
	OneHandSpear = 2,
	OneHandSword = 3,
	OneHandAxe = 4,
	OneHandBluntWeapon = 5,
	OneHandStaff = 6,
	OneHandWhip = 7,
	Book = 8,
	TwoHandedSpear = 9,
	TwoHandedSword = 10,
	TwoHandedAxe = 11,
	TwoHandedBluntWeapon = 12,
	TwoHandedStaff = 13,
	TwoHandedWhip = 14,
	Pistol = 15,
	LongGun = 16,
	Bow = 17,
	Crossbow = 18,
	Orb = 19,
	Wand = 20,
	DualWield = 21,
	WoodenShield = 22,
	MetalShield = 23,
	Monster = 24
}

[XmlRoot(ElementName = "Item")]
public class Item : IItem
{
	[XmlAttribute(AttributeName = "id")]
	public int Id { get; set; }
	[XmlAttribute(AttributeName = "modelId")]
	public int ModelId { get; set; }
	[XmlAttribute(AttributeName = "name")]
	public string Name { get; set; }
	[XmlAttribute(AttributeName = "desc")]
	public string Desc { get; set; }
	[XmlAttribute(AttributeName = "grade")]
	public int Grade { get; set; }
	[XmlAttribute(AttributeName = "maxEnchantCount")]
	public int MaxEnchantCount { get; set; }
	[XmlAttribute(AttributeName = "type")]
	public int Type { get; set; }
	[XmlAttribute(AttributeName = "secondType")]
	public int SecondType { get; set; }
	[XmlAttribute(AttributeName = "level")]
	public int Level { get; set; }
	[XmlAttribute(AttributeName = "tribeClass")]
	public int TribeClass { get; set; }
	[XmlAttribute(AttributeName = "jobClassType")]
	public int JobClassType { get; set; }
	[XmlAttribute(AttributeName = "jobClassTypeId")]
	public int JobClassTypeId { get; set; }
	[XmlAttribute(AttributeName = "weaponType")]
	public int WeaponType { get; set; }
	[XmlAttribute(AttributeName = "userType")]
	public int UserType { get; set; }
	[XmlAttribute(AttributeName = "stuffType")]
	public int StuffType { get; set; }
	[XmlAttribute(AttributeName = "skillId")]
	public int SkillId { get; set; }
	[XmlAttribute(AttributeName = "imageName")]
	public string ImageName { get; set; }
	[XmlAttribute(AttributeName = "smallImageName")]
	public string SmallImageName { get; set; }
	[XmlAttribute(AttributeName = "sortingType")]
	public int SortingType { get; set; }
	[XmlAttribute(AttributeName = "series")]
	public int Series { get; set; }
	[XmlAttribute(AttributeName = "isSell")]
	public int IsSell { get; set; }
	[XmlAttribute(AttributeName = "isExchange")]
	public int IsExchange { get; set; }
	[XmlAttribute(AttributeName = "isDiscard")]
	public int IsDiscard { get; set; }
	[XmlAttribute(AttributeName = "material")]
	public int Material { get; set; }
	[XmlAttribute(AttributeName = "durable")]
	public int Durable { get; set; }
	[XmlAttribute(AttributeName = "price")]
	public int Price { get; set; }
	[XmlElement(ElementName = "Inherit")]
	public List<Inherit> Inherits { get; set; }

	[XmlIgnore]
	public long Count { get; set; } = 1;

	public void UpdateAmount(int amount)
	{
		Count = amount;
	}

	public long GetAmount()
	{
		return Count;
	}

	public ITEM_INHERITS GetInherits()
	{
		var inherit = new ITEM_INHERITS()
		{
			inherits = new ITEM_INHERIT[25]
		};
		for (int i = 0; i < Inherits.Count; i++)
		{
			inherit.inherits[i] = new ITEM_INHERIT()
			{
				type = 0,
				typeID = (uint)Inherits[i].typeID,
				value = Inherits[i].val
			};
		}
		return inherit;
	}

	public INVENTORY_ITEM AsInventoryItem(int slot = 0)
	{
		return new INVENTORY_ITEM()
		{
			slot = slot,
			typeID = Id,
			count = Count,
			durability = 1500,
			mdurability = 1500,
			remainExpiryTime = -1,
			grade = Grade,
			inherits = GetInherits()
		};
	}

	public EQUIP_ITEM AsEquipItem()
	{
		return new EQUIP_ITEM()
		{
			itemTypeID = Id,
			equipSlot = (byte)GetEquipSlot(),
			wiredSlot = 0,
			mdurability = 200,
			durability = 200,
			grade = Grade,
			inherits = GetInherits()
		};
	}

	public EQUIP_SLOT GetEquipSlot()
	{
		switch((ItemType)Type)
		{
			case ItemType.MainEquipment: return EQUIP_SLOT.MAIN_EQUIP;
			case ItemType.AuxiliaryEquipment: return EQUIP_SLOT.SUB_EQUIP;
			case ItemType.RemoteEquipment: return EQUIP_SLOT.RANGE_MAIN_EQUIP;
			case ItemType.ConsumableEquipment: return EQUIP_SLOT.SPENDING;
			case ItemType.Head: return EQUIP_SLOT.HEAD;
			case ItemType.Forehead: return EQUIP_SLOT.FOREHEAD;
			case ItemType.Eyes: return EQUIP_SLOT.EYE;
			case ItemType.Mouth: return EQUIP_SLOT.MOUTH;
			case ItemType.Neck: return EQUIP_SLOT.NECK;
			case ItemType.Shoulder: return EQUIP_SLOT.SHOULDER;
			case ItemType.Top: return EQUIP_SLOT.UPPER_BODY;
			case ItemType.Hand: return EQUIP_SLOT.HAND;
			case ItemType.Waist: return EQUIP_SLOT.WAIST;
			case ItemType.Bottom: return EQUIP_SLOT.LOWER_BODY;
			case ItemType.Feet: return EQUIP_SLOT.FOOT;
			case ItemType.Artifact: return EQUIP_SLOT.RELIC;
			case ItemType.Ring1: return EQUIP_SLOT.RING_1;
			case ItemType.Ring2: return EQUIP_SLOT.RING_2;
			case ItemType.Accessory1: return EQUIP_SLOT.ACCESSORY_1;
			case ItemType.Accessory2: return EQUIP_SLOT.ACCESSORY_2;
			default: return EQUIP_SLOT.NONE;
		}
	}

	public EQUIPMENT EquipChangePart(EQUIPMENT_TYPE actionType, int invSlot)
	{
		return new EQUIPMENT()
		{
			type = (byte)actionType,
			invSlot = invSlot,
			equipSlot = (byte)GetEquipSlot()
		};
	}
}

public class ItemNA
{
	[XmlAttribute]
	public int typeID { get; set; }
	[XmlAttribute]
	public int modelId { get; set; }
	[XmlAttribute]
	public string note1 { get; set; }
	[XmlAttribute]
	public string name { get; set; }
	[XmlAttribute]
	public string note2 { get; set; }
	[XmlAttribute]
	public string desc { get; set; }
	[XmlAttribute]
	public int category { get; set; }
	[XmlAttribute]
	public int setitem { get; set; }
	[XmlAttribute]
	public int itemClass { get; set; }
	[XmlAttribute]
	public int maxEnchantCount { get; set; }
	[XmlAttribute]
	public int type { get; set; }
	[XmlAttribute]
	public int secondType { get; set; }
	[XmlAttribute]
	public int equipableSlot { get; set; }
	[XmlAttribute]
	public int level { get; set; }
	[XmlAttribute]
	public int tribeClass { get; set; }
	[XmlAttribute]
	public int sex { get; set; }
	[XmlAttribute]
	public int jobClassType { get; set; }
	[XmlAttribute]
	public int jobClassTypeId { get; set; }
	[XmlAttribute]
	public int weaponType { get; set; }
	[XmlAttribute]
	public int userType { get; set; }
	[XmlAttribute]
	public int stuffType { get; set; }
	[XmlAttribute]
	public int skillId { get; set; }
	[XmlAttribute]
	public string imageName { get; set; }
	[XmlAttribute]
	public string smallImageName { get; set; }
	[XmlAttribute]
	public string bigImageName { get; set; }
	[XmlAttribute]
	public string cardImageName { get; set; }
	[XmlAttribute]
	public int sortingType { get; set; }
	[XmlAttribute]
	public int series { get; set; }
	[XmlAttribute]
	public int isSell { get; set; }
	[XmlAttribute]
	public int isExchange { get; set; }
	[XmlAttribute]
	public int isDiscard { get; set; }
	[XmlAttribute]
	public int isBankMove { get; set; }
	[XmlAttribute]
	public int material { get; set; }
	[XmlAttribute]
	public int mdurability { get; set; }
	[XmlAttribute]
	public int price { get; set; }
	[XmlAttribute]
	public int belong { get; set; }
	[XmlAttribute]
	public int Randombox_id { get; set; }
	[XmlAttribute]
	public int ItemValueLV { get; set; }
	[XmlAttribute]
	public int Enchant { get; set; }
	[XmlAttribute]
	public int attribute { get; set; }
	[XmlAttribute]
	public int eggItemTypeID { get; set; }
	[XmlAttribute]
	public int coolTimetype { get; set; }
	[XmlAttribute]
	public int coolTimeGroup { get; set; }
	[XmlAttribute]
	public int coolTime { get; set; }
	[XmlAttribute]
	public int Cachetype { get; set; }
	[XmlAttribute]
	public int CacheTime { get; set; }
	[XmlAttribute]
	public int Stack { get; set; }
	[XmlAttribute]
	public int Minimum { get; set; }
	[XmlAttribute]
	public int Maximum { get; set; }
	[XmlAttribute]
	public int hitDuration { get; set; }

	[XmlElement]
	public List<Inherit> Inherits { get; set; }

	public INVENTORY_ITEM AsInventoryItem()
	{
		var item =  new INVENTORY_ITEM()
		{
			slot = 0,
			typeID = typeID,
			count = 1,
			durability = mdurability,
			mdurability = mdurability,
			remainExpiryTime = 0,
			grade = 1,
			inherits = new ITEM_INHERITS()
			{
				inherits = new ITEM_INHERIT[25]
			}
		};

		for (int i = 0; i < Inherits.Count; i++)
		{
			item.inherits.inherits[i] = new ITEM_INHERIT()
			{
				type = 0,
				typeID = (uint)Inherits[i].typeID,
				value = Inherits[i].val
			};
		}
		return item;
	}

}

public class Inherit
{
	public int typeID { get; set; }
	public int val { get; set; }
}
