using System.Xml.Serialization;

namespace kakia_lime_odyssey_server.Models.SkillXML;

public class XmlSkill
{ 
	[XmlAttribute(AttributeName = "id")] public int Id { get; set; } 
	[XmlAttribute(AttributeName = "type")] public string Type { get; set; } 
	[XmlAttribute(AttributeName = "name")] public string Name { get; set; } 
	[XmlAttribute(AttributeName = "nameEng")] public string NameEng { get; set; } 
	[XmlAttribute(AttributeName = "target")] public int Target { get; set; } 
	[XmlAttribute(AttributeName = "targetAttribute")] public int TargetAttribute { get; set; } 
	[XmlAttribute(AttributeName = "class")] public string Class { get; set; } 
	[XmlAttribute(AttributeName = "castingTime")] public double CastingTime { get; set; } 
	[XmlAttribute(AttributeName = "coolTime")] public double CoolTime { get; set; } 
	[XmlAttribute(AttributeName = "imageName")] public string ImageName { get; set; } 
	[XmlAttribute(AttributeName = "slot")] public int Slot { get; set; } 
	[XmlAttribute(AttributeName = "comBoGauge")] public int ComboGauge { get; set; } 
	[XmlAttribute(AttributeName = "isCombo")] public int IsCombo { get; set; }
	[XmlAttribute(AttributeName = "weaponOnBody")] public int WeaponOnBody { get; set; }
	[XmlAttribute(AttributeName = "skillStatusType")] public int SkillStatusType { get; set; }
	[XmlAttribute(AttributeName = "castingAttr")] public int CastingAttr { get; set; } 
	[XmlAttribute(AttributeName = "soundid")] public int SoundId { get; set; }
	[XmlAttribute(AttributeName = "range")] public double Range { get; set; } 
	[XmlAttribute(AttributeName = "chainSkill")] public int ChainSkill { get; set; } 
	[XmlAttribute(AttributeName = "series")] public int Series { get; set; } 
	[XmlAttribute(AttributeName = "motionDelay")] public double MotionDelay { get; set; }
	[XmlElement(ElementName = "Subject")] public SkillSubject Subject { get; set; } 
}
