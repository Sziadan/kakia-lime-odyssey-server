using System.Xml.Serialization;

namespace kakia_lime_odyssey_server.Models;

[XmlRoot(ElementName = "Message")] 
public class XmlMessage
{
	[XmlAttribute(AttributeName = "ID")]
	public int ID { get; set; }
	[XmlAttribute(AttributeName = "Text")]
	public string Text { get; set; } 
	[XmlAttribute(AttributeName = "Note_File")]
	public string NoteFile { get; set; } 
	[XmlAttribute(AttributeName = "Note_Field")]
	public string NoteField { get; set; } 
}

[XmlRoot(ElementName = "Localization")]
public class XmlLocalization
{
	[XmlElement(ElementName = "Message")] 
	public List<XmlMessage> Messages { get; set; }

	public static List<XmlMessage> GetEntries()
	{
		XmlSerializer serializer = new XmlSerializer(typeof(XmlLocalization));
		using FileStream fileStream = new FileStream("db/Localization.xml", FileMode.Open);
		var loc = (XmlLocalization)serializer.Deserialize(fileStream)!;

		return loc.Messages;
	}
}

