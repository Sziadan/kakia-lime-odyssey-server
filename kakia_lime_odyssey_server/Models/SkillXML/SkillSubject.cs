using System.Xml.Serialization;

namespace kakia_lime_odyssey_server.Models.SkillXML;

public class SkillSubject
{ 
	[XmlElement(ElementName = "SubjectList")] 
	public List<SkillSubjectList> SubjectLists { get; set; } 
}


public class SkillSubjectList 
{ 
	[XmlAttribute(AttributeName = "subjectLevel")] public int SubjectLevel { get; set; } 
	[XmlAttribute(AttributeName = "detail")] public string Detail { get; set; } 
	[XmlAttribute(AttributeName = "range")] public double Range { get; set; } 
	[XmlAttribute(AttributeName = "castingTime")] public double CastingTime { get; set; } 
	[XmlAttribute(AttributeName = "coolTime")] public double CoolTime { get; set; } 
	[XmlAttribute(AttributeName = "useHP")] public int UseHP { get; set; } 
	[XmlAttribute(AttributeName = "useHPStyle")] public int UseHPStyle { get; set; } 
	[XmlAttribute(AttributeName = "useSP")] public int UseSP { get; set; } 
	[XmlAttribute(AttributeName = "useSPStyle")] public int UseSPStyle { get; set; } 
	[XmlAttribute(AttributeName = "useLP")] public int UseLP { get; set; } 
	[XmlAttribute(AttributeName = "useLPStyle")] public int UseLPStyle { get; set; } 
	[XmlAttribute(AttributeName = "useMP")] public int UseMP { get; set; } 
	[XmlAttribute(AttributeName = "useMPStyle")] public int UseMPStyle { get; set; } 
	[XmlElement(ElementName = "subject")] public List<SkillSubjectDetail> Subjects { get; set; } 
}

public class SkillSubjectDetail
{
	[XmlAttribute(AttributeName = "skillId")]
	public int SkillId { get; set; }
	[XmlAttribute(AttributeName = "level")]
	public int Level { get; set; }
}