using kakia_lime_odyssey_server.Models.MonsterXML;
using System.Xml.Serialization;

namespace kakia_lime_odyssey_server.Models.PcStatusXML;

[XmlRoot("PcStatus")]
public class PcStatus
{
	[XmlElement("Exp")]
	public List<Exp> Exps { get; set; }

	public static Dictionary<int, Exp> GetEntries()
	{
		XmlSerializer serializer = new XmlSerializer(typeof(PcStatus));
		using FileStream fileStream = new FileStream("db/xmls/PcStatus.xml", FileMode.Open);
		var pcStatus = (PcStatus)serializer.Deserialize(fileStream)!;

		var entries = new Dictionary<int, Exp>();

		foreach (var exp in pcStatus.Exps)		
			entries.Add(exp.Level, exp);		

		return entries;
	}
}

public class Exp
{
	[XmlAttribute("level")]
	public int Level { get; set; }

	[XmlAttribute("exp")]
	public int ExpPoints { get; set; }

	[XmlAttribute("lifeExp")]
	public int LifeExp { get; set; }

	[XmlAttribute("combatExp")]
	public int CombatExp { get; set; }
}
