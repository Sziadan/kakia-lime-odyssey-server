using kakia_lime_odyssey_server.Models.SkillXML;
using System.Xml.Serialization;

namespace kakia_lime_odyssey_server.Models;

[XmlRoot(ElementName = "SkillInfo")]
public class SkillInfo
{
	[XmlElement(ElementName = "Skill")]
	public List<XmlSkill> Skills { get; set; }

	public static List<XmlSkill> GetSkills()
	{
		XmlSerializer serializer = new XmlSerializer(typeof(SkillInfo));
		using FileStream fileStream = new FileStream("db/xmls/SkillInfo.xml", FileMode.Open);
		var info = (SkillInfo)serializer.Deserialize(fileStream)!;

		return info.Skills;
	}
}