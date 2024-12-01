using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct SAVED_LIFE_JOB_STATUS
{
	public byte lv;
	public uint exp;
	public ushort statusPoint;
	public ushort idea;
	public ushort mind;
	public ushort craft;
	public ushort sense;
}


public class ModLifeJobStatus
{
	public byte lv;
	public uint exp;
	public ushort statusPoint;
	public ushort idea;
	public ushort mind;
	public ushort craft;
	public ushort sense;

	public ModLifeJobStatus(SAVED_LIFE_JOB_STATUS status)
	{
		lv = status.lv;
		exp = status.exp;
		statusPoint = status.statusPoint;
		idea = status.idea;
		mind = status.mind;
		craft = status.craft;
		sense = status.sense;
	}

	public SAVED_LIFE_JOB_STATUS AsStruct()
	{
		return new SAVED_LIFE_JOB_STATUS()
		{
			lv = lv,
			exp = exp,
			statusPoint = statusPoint,
			idea = idea,
			mind = mind,
			craft = craft,
			sense = sense
		};
	}
}