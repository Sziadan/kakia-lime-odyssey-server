using System.Xml.Serialization;
namespace kakia_lime_odyssey_server.Models.MonsterXML;

[XmlRoot("ModelsInfo")]
public class ModelInfo
{
	[XmlAttribute("realShadowWeight")]
	public int RealShadowWeight { get; set; }

	[XmlAttribute("sphereShadowWeight")]
	public int SphereShadowWeight { get; set; }

	[XmlAttribute("shadowBlock")]
	public int ShadowBlock { get; set; }

	[XmlAttribute("modelShadowVertexCount")]
	public int ModelShadowVertexCount { get; set; }

	[XmlAttribute("modelShadowDepth")]
	public float ModelShadowDepth { get; set; }

	[XmlAttribute("modelShadowTextureSize")]
	public int ModelShadowTextureSize { get; set; }

	[XmlElement("Model")]
	public List<Model> Models { get; set; }

	public static List<Model> GetEntries()
	{
		XmlSerializer serializer = new XmlSerializer(typeof(ModelInfo));
		using FileStream fileStream = new FileStream("db/xmls/ModelsInfo.xml", FileMode.Open);
		var modelsInfo = (ModelInfo)serializer.Deserialize(fileStream)!;

		return modelsInfo.Models;
	}
}

public class Model
{
	[XmlAttribute("typeId")]
	public int TypeId { get; set; }

	[XmlAttribute("category")]
	public string Category { get; set; }

	[XmlAttribute("nifName")]
	public string NifName { get; set; }

	[XmlAttribute("fileName")]
	public string FileName { get; set; }

	[XmlAttribute("tciName")]
	public string TciName { get; set; }

	[XmlAttribute("grass")]
	public int Grass { get; set; }

	[XmlAttribute("useNif")]
	public int UseNif { get; set; }

	[XmlAttribute("weaponType")]
	public int WeaponType { get; set; }

	[XmlAttribute("height")]
	public float Height { get; set; }

	[XmlAttribute("scale")]
	public float Scale { get; set; }

	[XmlAttribute("actorRadius")]
	public float ActorRadius { get; set; }

	[XmlAttribute("autoEffectScale")]
	public float AutoEffectScale { get; set; }

	[XmlAttribute("autoEffectPos")]
	public string AutoEffectPos { get; set; }

	[XmlAttribute("runFreq")]
	public float RunFreq { get; set; }

	[XmlAttribute("walkFreq")]
	public float WalkFreq { get; set; }

	[XmlAttribute("selectDecalZoom")]
	public float SelectDecalZoom { get; set; }

	[XmlAttribute("soundId")]
	public int SoundId { get; set; }

	[XmlAttribute("noAni")]
	public int NoAni { get; set; }

	[XmlAttribute("noShadow")]
	public int NoShadow { get; set; }

	[XmlAttribute("type")]
	public int Type { get; set; }

	[XmlElement("Ani")]
	public List<Ani> Anis { get; set; }
}

public class Ani
{
	[XmlAttribute("id")]
	public int Id { get; set; }

	[XmlAttribute("mixId")]
	public int MixId { get; set; }

	[XmlAttribute("effectId")]
	public int EffectId { get; set; }

	[XmlAttribute("duration")]
	public float Duration { get; set; }

	[XmlAttribute("hitTime")]
	public float HitTime { get; set; }
}
