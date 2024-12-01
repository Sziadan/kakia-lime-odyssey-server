using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential)]
public struct CLIENT_PC
{
	public SAVED_STATUS_PC_KR status { get; set; }
	public APPEARANCE_PC_KR appearance { get; set; }
}


public class ModClientPC
{
	public ModSavedStatus status { get; set; }
	public ModAppearance appearance { get; set; }

	public ModClientPC(CLIENT_PC clientpc)
	{
		status = new ModSavedStatus(clientpc.status);
		appearance = new ModAppearance(clientpc.appearance);
	}

	public CLIENT_PC AsStruct()
	{
		return new CLIENT_PC()
		{
			status = status.AsStruct(),
			appearance = appearance.AsStruct()
		};
	}
}