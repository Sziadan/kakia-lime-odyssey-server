using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential)]
public struct VELOCITIES
{
	public float ratio;
	public float run;
	public float runAccel;
	public float walk;
	public float walkAccel;
	public float backwalk;
	public float backwalkAccel;
	public float swim;
	public float swimAccel;
	public float backSwim;
	public float backSwimAccel;
}
