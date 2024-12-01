using kakia_lime_odyssey_logging;
using System.Net;
using System.Net.Sockets;

namespace kakia_lime_odyssey_network
{
	public class SocketClient
	{
		public Func<RawPacket, Task>? PacketReceived;

		private Socket _socket { get; set; }
		private byte[] _buffer { get; set; }
		private int _position = 0;
		public bool IsAlive { get; set; }
		public bool UseCrypto { get; set; }

		public int Id { get; set; }		

		public SocketClient(Socket socket)
		{
			IsAlive = true;
			_socket = socket;
			_buffer = new byte[1024 * 10];
			Id = socket.Handle.ToInt32();
		}

		public async Task BeginRead()
		{
			try
			{
				var seg = new ArraySegment<byte>(_buffer, _position, _buffer.Length - _position);
				var len = await _socket.ReceiveAsync(seg);
				if (len <= 0)
				{
					IsAlive = false;
					Logger.Log($"{GetIP()} disconnected.");
					return;
				}
				await HandleData(len);
			} catch (Exception ex)
			{
				Logger.Log(ex);
				IsAlive = false;
			}
		}

		private async Task HandleData(int len)
		{
			try
			{
				var packets = RawPacket.ParsePackets(_buffer[_position..len], UseCrypto);
				foreach(var packet in packets)
					await PacketReceived!.Invoke(packet);
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
			}
			await BeginRead();
		}

		public async Task Send(byte[] packet)
		{
			await _socket.SendAsync(packet);
		}

		public string GetIP()
		{
			if (!_socket.Connected)
				return string.Empty;
			var ip = _socket.RemoteEndPoint as IPEndPoint;
			return ip.Address.ToString();
		}
	}
}