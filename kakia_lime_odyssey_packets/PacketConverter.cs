using kakia_lime_odyssey_packets.Packets.Interface;
using kakia_lime_odyssey_utils.Extensions;
using System.Drawing;
using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets;

public static class PacketConverter
{
	public static T Extract<T>(byte[] data) where T : struct
	{
		/*
		int expectedSize = Marshal.SizeOf(typeof(T));
		if (data.Length !=  expectedSize)
		{
			Console.WriteLine($"[Warning] Expected size of {typeof(T).FullName} is {expectedSize} but the input data has the size {data.Length}.");
			Console.WriteLine("===========================================");
			Console.WriteLine(data.ToFormatedHexString());
			Console.WriteLine(Environment.NewLine);
			//throw new Exception($"Expected size of {typeof(T).FullName} is {expectedSize} but the input data has the size {data.Length}.");
		}
		*/

		GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
		T packet = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T))!;
		handle.Free();
		return packet;
	}

	public static byte[] AsBytes<T>(T packet) where T : struct
	{
		int size = Marshal.SizeOf(packet);
		byte[] arr = new byte[size];

		IntPtr ptr = IntPtr.Zero;
		try
		{
			ptr = Marshal.AllocHGlobal(size);
			Marshal.StructureToPtr(packet, ptr, false);
			Marshal.Copy(ptr, arr, 0, size);
		}
		finally
		{
			Marshal.FreeHGlobal(ptr);
		}
		return arr;
	}

	public static byte[] AsBytes(IPacketVar packet)
	{
		int size = Marshal.SizeOf(packet);
		byte[] arr = new byte[size];

		IntPtr ptr = IntPtr.Zero;
		try
		{
			ptr = Marshal.AllocHGlobal(size);
			Marshal.StructureToPtr(packet, ptr, false);
			Marshal.Copy(ptr, arr, 0, size);
		}
		finally
		{
			Marshal.FreeHGlobal(ptr);
		}
		return arr;
	}

	public static byte[] AsBytes(IPacketFixed packet)
	{
		int size = Marshal.SizeOf(packet);
		byte[] arr = new byte[size];

		IntPtr ptr = IntPtr.Zero;
		try
		{
			ptr = Marshal.AllocHGlobal(size);
			Marshal.StructureToPtr(packet, ptr, false);
			Marshal.Copy(ptr, arr, 0, size);
		}
		finally
		{
			Marshal.FreeHGlobal(ptr);
		}
		return arr;
	}
}
