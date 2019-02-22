using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using MinecartSharp.Enums;
using MinecartSharp.Networking.Packets;
using MinecartSharp.Objects;

namespace MinecartSharp.Networking.Objects
{
    class Client
    {
        public TcpClient TcpClient { get; }
        public ConnectionState State = ConnectionState.Unknown;

        private readonly Timer _timer = new Timer(); 

        public string Username { get; set; }

        public Client(TcpClient client)
        {
            TcpClient = client;
        }

        public void Kick(string reason)
        {
            var chat = new Chat() {Text = reason};

            var message = new Message(TcpClient.GetStream());
            var packet = PacketHandler.GetPacket(0x1A, ConnectionState.Play);

            packet.Write(this, message, chat);

            message.Dispose();
            TcpClient.Close();
        }

        public void SendData(byte[] data)
        {
            try
            {
                var stream = TcpClient.GetStream();
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }
            catch
            {
                // ignored
            }
        }

        public void StartKeepAlive()
        {
            _timer.Elapsed += (sender, args) =>
            {
                var message = new Message(TcpClient.GetStream());
                new KeepAlive().Write(this, message);

                message.Dispose();
            };

            _timer.Interval = 10 * 1000;
            _timer.Start();
        }

        public void StopKeepAlive()
        {
            _timer.Stop();
        }
    }
}
