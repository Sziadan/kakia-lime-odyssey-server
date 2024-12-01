using System.Text;

namespace kakia_lime_odyssey_utils.Extensions;

public static class ByteExt
{
	public static byte[] ToLE(this byte[] data, int offset, int len)
	{
		int end = offset + len;
		return data[offset..end].Reverse().ToArray();
	}

	public static string ToFormatedHexString(this byte[] b)
	{
		if (b == null)
			return string.Empty;

		StringBuilder sb = new();
		string cleaned = Convert.ToHexString(b);

		int c = 0;
		for (int i = 0; i + 1 < cleaned.Length; i += 2)
		{
			sb.Append(cleaned[i]);
			sb.Append(cleaned[i + 1]);
			sb.Append(c % 16 == 15 ? Environment.NewLine : ' ');
			c++;
		}

		return sb.ToString();
	}

	public static string ToAscii(this byte[] b)
	{
		StringBuilder sb = new StringBuilder();
		byte[] bytes = new byte[b.Length];
		Buffer.BlockCopy(b, 0, bytes, 0, b.Length);

		for (int i = 0; i < bytes.Length; i++)
			if (bytes[i] < 0x30)
				bytes[i] = 0x2E;

		string temp = Encoding.ASCII.GetString(bytes);
		for (int i = 1; i <= temp.Length; i++)
		{
			if (i % 16 == 0 && i != 0)
			{
				sb.Append(temp[i - 1]);
				sb.Append(Environment.NewLine);
			}
			else
			{
				sb.Append(temp[i - 1]);
				sb.Append(' ');
			}
		}

		return sb.ToString();
	}

	public static void Replace(this byte[] b, byte[] pattern, byte[] replacement)
	{
		for (int i = 0; i + pattern.Length < b.Length; i++)
		{
			bool match = true;
			for (int j = 0; j < pattern.Length; j++)
			{
				if (b[i + j] != pattern[j])
				{
					match = false;
					break;
				}
			}

			if (match)
			{
				Buffer.BlockCopy(replacement, 0, b, i, pattern.Length);
				i += pattern.Length;
			}
		}
	}
}
