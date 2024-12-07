using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_server.Models;

namespace kakia_lime_odyssey_server.Interfaces;

public interface IEntity
{
	public long GetId();
	public FPOS GetPosition();
	public FPOS GetDirection();
	public EntityStatus GetEntityStatus();

	/// <summary>
	/// Can be used to both deal damage and heal the target.
	/// Decided to go this route in case we want it to be possible
	/// to heal undead enemies in order to damage them.
	/// Or if it's possible to change element of the player where healing
	/// magic causes them to take damage. (Like Undead Armor in Ragnarok Online)
	/// </summary>
	/// <param name="damage">Negative value to deal damage, positive value to heal.</param>
	/// <returns></returns>
	public DamageResult UpdateHealth(int healthChange);
	public bool AddExp(ulong exp);

	public List<Item> GetLoot();
	public void Loot(Item item);
}
