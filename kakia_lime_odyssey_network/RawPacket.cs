using kakia_lime_odyssey_logging;
using kakia_lime_odyssey_network.Crypto;
using kakia_lime_odyssey_packets;
using System.Data;

namespace kakia_lime_odyssey_network;

public class RawPacket
{
	public PacketType PacketId { get; set; }
	public PacketType_REV345 PacketId_REV345 { get; set; }
	public UInt16 Size { get; set; }
	public byte[] Payload { get; set; }

	public static List<RawPacket> ParsePackets(byte[] data, bool useCrypto)
	{
		List<RawPacket> packets = new();
		int pos = 0;
		while(pos < data.Length)
		{
			try
			{
				if (!useCrypto)
				{
					var p1 = new RawPacket()
					{
						Size = (ushort)data.Length,
						Payload = data[2..],
						PacketId_REV345 = (PacketType_REV345)BitConverter.ToUInt16(data.AsSpan(0, 2))
					};
					p1.PacketId = (PacketType)Enum.Parse(typeof(PacketType), p1.PacketId_REV345.ToString());
					packets.Add(p1);
					break;
				}

				int totalSize = BitConverter.ToUInt16(data, pos);
				var p = new RawPacket()
				{
					Size = BitConverter.ToUInt16(data.AsSpan(pos + 2, 2))
				};

				var temp = AesLime.Decrypt(data[(pos + 4)..(pos + p.Size + 4)]);

				// Check if it has a payload, some packets don't
				if (temp.Length > 2)
					p.Payload = temp[2..].ToArray();

				p.PacketId = (PacketType)BitConverter.ToUInt16(temp.AsSpan(0, 2));

				pos += totalSize;
				packets.Add(p);
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				throw new Exception("Crypto failed.", ex);
			}
		}
		return packets;
	}

	public string GetName()
	{
		return Enum.GetName(typeof(PacketType), PacketId)!;
	}
}
