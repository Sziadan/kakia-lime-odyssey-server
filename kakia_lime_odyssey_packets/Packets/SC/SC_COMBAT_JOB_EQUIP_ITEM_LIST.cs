﻿using kakia_lime_odyssey_packets.Packets.Models;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SC_COMBAT_JOB_EQUIP_ITEM_LIST
{
	public EQUIP_ITEM[] equipList;
}
