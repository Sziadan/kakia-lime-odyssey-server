

using kakia_lime_odyssey_logging;
using kakia_lime_odyssey_server.Models;
using kakia_lime_odyssey_server.Network;
using Newtonsoft.Json;
using System.Text;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

Config cfg;
if (!File.Exists("config.json"))
{
	cfg = new Config()
	{
		ServerIP = "127.0.0.1",
		Port = 9676,
		Crypto = true
	};
	var json = JsonConvert.SerializeObject(cfg, Formatting.Indented);
	File.WriteAllText("config.json", json);
	Logger.Log("Default config.json generated, if you do not wish to run with the default values then please open it and adjust it to your liking.", LogLevel.Information);
}
else
{
	try
	{
		cfg = JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"))!;
	}
	catch (Exception ex) 
	{
		Logger.Log(ex);
		Logger.Log("Failed to load config file, probably malformed config.json", LogLevel.Exception);
		return;
	}
}


Logger.Log("===================================================================================================");
Logger.Log("                         Welcome to the Kakia LimeOdyssey Server!");
Logger.Log("Please note, this server is currently intended to be used with the Korean CBT3 client (rev 211).");
Logger.Log("===================================================================================================");

Logger.SetLogLevel(LogLevel.Debug);
kakia_lime_odyssey_network.Handler.PacketHandlers.LoadPacketHandlers("kakia_lime_odyssey_server.PacketHandlers");
LimeServer server = new(cfg);
CancellationTokenSource ct = new();

_ = server.Run(ct.Token);

Logger.Log("==== [Server is now running, press Q to quit gracefully] ====", LogLevel.Information);
while (Console.ReadKey().KeyChar.ToString().ToLower() != "q")
{
	await Task.Delay(1000);
}
server.Stop();
ct.Cancel();