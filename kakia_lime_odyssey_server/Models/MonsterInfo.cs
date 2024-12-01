using kakia_lime_odyssey_server.Models.MonsterXML;
using System.Xml.Serialization;

namespace kakia_lime_odyssey_server.Models;

[XmlRoot(ElementName = "NPC")]
public class MonsterInfo
{
	[XmlElement(ElementName = "Monster")]
	public List<XmlMonster> Monsters {	get; set; }

	public static List<XmlMonster> GetEntries()
	{
		XmlSerializer serializer = new XmlSerializer(typeof(MonsterInfo));
		using FileStream fileStream = new FileStream("db/monster.xml", FileMode.Open);
		var mobInfo = (MonsterInfo)serializer.Deserialize(fileStream)!;


		var loc = XmlLocalization.GetEntries();

		foreach(var mob in mobInfo.Monsters)
		{
			var translation = loc.FirstOrDefault(m => m.ID.ToString().Equals(mob.Name));
			if (translation is not null)
			{
				mob.Name = translation.Text;
			}
		}

		return mobInfo.Monsters;
	}
}
