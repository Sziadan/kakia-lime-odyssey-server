namespace kakia_lime_odyssey_server.Models;

public interface INPC
{
	public NPC_TYPE GetNPCType();
}

public enum NPC_TYPE
{
	NPC,
	MOB
}