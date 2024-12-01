using kakia_lime_odyssey_packets.Packets.Models;

namespace kakia_lime_odyssey_server.Models;

public class WorldPosition
{
	public uint ZoneID { get; set; }
	public FPOS Position { get; set; }
	public FPOS Direction { get; set; }

	public WorldPosition()
	{
		Position = new FPOS();
		Direction = new FPOS();
	}
}
