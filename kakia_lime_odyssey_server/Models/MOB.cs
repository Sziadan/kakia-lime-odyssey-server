using kakia_lime_odyssey_packets.Packets.Models;
using kakia_lime_odyssey_packets.Packets.SC;

namespace kakia_lime_odyssey_server.Models;

public class MOB : INPC
{
	public int Id { get; set; }
	public COMMON_STATUS Status { get; set; }
	public uint ZoneId { get; set; }
	public FPOS Pos { get; set; }
	public FPOS Dir { get; set; }
	public APPEARANCE_MONSTER Appearance { get; set; }
	public int RaceRelationState;
	public byte StopType;

	public NPC_TYPE GetNPCType()
	{
		return NPC_TYPE.MOB;
	}

	public SC_ENTER_SIGHT_MONSTER GetEnterSight()
	{
		return new()
		{
			enter_zone = new()
			{
				objInstID = Id,
				pos = Pos,
				dir = Dir
			},
			appearance = Appearance,
			raceRelationState = RaceRelationState,
			stopType = StopType
		};
	}
}
