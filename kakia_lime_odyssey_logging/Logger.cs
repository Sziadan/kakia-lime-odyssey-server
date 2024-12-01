using kakia_lime_odyssey_utils.Extensions;
using System.Reflection.Emit;

namespace kakia_lime_odyssey_logging;

public static class Logger
{
	private static LogLevel _level;

	public static void SetLogLevel(LogLevel level)
	{
		_level = level;
	}

	public static void Log(string message)
	{
		Prefix(LogLevel.Information);
		Console.WriteLine(message);
	}

	public static void LogPck(byte[] packet)
	{
		Log("== Packet ==", LogLevel.Debug);
		Console.WriteLine(packet.ToFormatedHexString());
	}

	public static void Log(Exception ex)
	{
		if (LogLevel.Exception > _level) return;

		Prefix(LogLevel.Exception);
		Console.WriteLine(ex.Message);
		Console.WriteLine("== StackTrace ==");
		Console.WriteLine(ex.StackTrace);
	}

	public static void Log(string message, LogLevel level)
	{
		if (level > _level) return;

		Prefix(level);
		Console.WriteLine(message);
	}

	private static void Prefix(LogLevel level)
	{
		switch (level)
		{
			case LogLevel.Debug:
				Console.ForegroundColor = ConsoleColor.Cyan;
				break;

			case LogLevel.Information:
				Console.ForegroundColor = ConsoleColor.White;
				break;

			case LogLevel.Warning:
				Console.ForegroundColor = ConsoleColor.Yellow;
				break;

			case LogLevel.Error:
			case LogLevel.Exception:
				Console.ForegroundColor = ConsoleColor.Red;
				break;
		}

		Console.Write($"[{level}] ");
		Console.ResetColor();
	}
}
