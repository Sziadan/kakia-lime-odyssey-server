using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential)]
public struct COMMON_STATUS
{
	public byte lv;
	public uint hp;
	public uint mhp;
	public uint mp;
	public uint mmp;
}

public class ModCommonStatus
{
	public byte lv;
	public uint hp;
	public uint mhp;
	public uint mp;
	public uint mmp;


	public ModCommonStatus() { }

	public ModCommonStatus(COMMON_STATUS commonStatus)
	{
		lv = commonStatus.lv;
		hp = commonStatus.hp;
		mhp = commonStatus.mhp;
		mp = commonStatus.mp;
		mmp = commonStatus.mmp;
	}

	public COMMON_STATUS AsStruct()
	{
		return new COMMON_STATUS
		{
			lv = lv,
			hp = hp,
			mhp = mhp,
			mp = mp,
			mmp = mmp
		};
	}
}