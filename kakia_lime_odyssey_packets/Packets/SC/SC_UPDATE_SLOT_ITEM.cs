﻿using kakia_lime_odyssey_packets.Packets.Interface;
using kakia_lime_odyssey_packets.Packets.Models;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.SC;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SC_UPDATE_SLOT_ITEM : IPacketFixed
{
	public ITEM_SLOT slot;
	public long count;
}