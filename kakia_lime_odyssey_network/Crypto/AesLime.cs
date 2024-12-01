using kakia_lime_odyssey_logging;
using System.Security.Cryptography;

namespace kakia_lime_odyssey_network.Crypto;

public static class AesLime
{
	private static readonly byte[] Key =
	{
		0x33, 0x26, 0x75, 0x35, 0x78, 0x70, 0x5E, 0x29,
		0x70, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
	};

	private static readonly byte[] IV = new byte[16];
	private static readonly Aes aes = Aes.Create();

	public static byte[] Decrypt(byte[] payload)
	{
		try
		{
			aes.Key = Key;
			return aes.DecryptCbc(payload, IV, PaddingMode.None);
		}
		catch (Exception ex)
		{
			Logger.Log(ex);
			return Array.Empty<byte>();
		}
	}
}
