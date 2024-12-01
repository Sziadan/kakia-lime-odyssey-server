namespace kakia_lime_odyssey_utils.Extensions
{
	public static class StringExt
	{
		public static byte[] ToByteArray(this string s)
		{
			Span<char> str = stackalloc char[s.Length];
			int len = 0;
			for (int i = 0; i < s.Length; i++)
			{
				switch (s[i])
				{
					case ' ':
					case '\t':
					case '\n':
					case '\r':
						break;

					default:
						str[len] = s[i];
						len++;
						break;
				}
			}

			byte[] bytes = new byte[len / 2];

			for (int bx = 0, sx = 0; bx < len / 2; ++bx, ++sx)
			{
				// Convert first half of byte
				char c = str[sx];
				bytes[bx] = (byte)((c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0')) << 4);

				// Convert second half of byte
				c = str[++sx];
				bytes[bx] |= (byte)(c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0'));
			}
			return bytes;
		}
	}
}
