namespace kakia_lime_odyssey_server.Models;

public class PlayerEquips
{
	public PlayerEquipment Combat { get; set; }
	public PlayerEquipment Life { get; set; }

	public PlayerEquips()
	{
		Combat = new PlayerEquipment();
		Life = new PlayerEquipment();
	}
}
