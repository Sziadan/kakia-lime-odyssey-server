namespace kakia_lime_odyssey_network.Interface;

public interface IItem
{
	public ulong GetId();
	public void UpdateAmount(ulong amount);
	public ulong GetAmount();
	public bool Stackable();
}

