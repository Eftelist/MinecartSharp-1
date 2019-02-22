using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MinecartSharp.Enums;
using MinecartSharp.Networking.Objects;
using MinecartSharp.Utils;

namespace MinecartSharp.Networking
{
    class Listener
    {
        private readonly Config _configuration;
        private readonly TcpListener _listener;

        public Listener(int port, Config configuration)
        {
            _configuration = configuration;

            _listener = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            _listener.Start();

            Logger.Log(LogType.Info, "Ready to accept connections.");

            while (true)
            {
                var client = _listener.AcceptTcpClient();
                var stream = client.GetStream();

                // Handle client in new task
                Logger.Log(LogType.Debug, "A new connection has been made.");

                Task.Run(() => HandleClient(client, stream));
            }
        }

        public void Stop()
        {
            _listener.Stop();
        }

        private void HandleClient(TcpClient tcpClient, NetworkStream stream)
        {
            Client client = new Client(tcpClient);
            MinecartSharp.GetServer().AddClient(client);

            // Main client connection loop
            while (client.TcpClient.Connected)
            {
                var message = new Message(stream);
                var receivedData = stream.Read(message.Data, 0, message.Data.Length);

                if (receivedData > 0)
                {
                    // Length    | Int        | Length of packet data + length of the packet ID 
                    // Packet Id | Int        | Id of the packet send
                    // Data      | Byte Array | Data of the packet
                    var length = message.ReadVarInt();
                    var packetId = message.ReadVarInt();

                    Logger.Log(LogType.Debug, "Received packet 0x" + packetId.ToString("X2"));

                    if (PacketHandler.Exists(packetId, client.State))
                    {
                        PacketHandler.GetPacket(packetId, client.State).Read(client, message);
                    }

                    message.Dispose();
                }
                else
                {
                    // client disconnected
                    break;
                }
            }

            // Close connection with client
            client.StopKeepAlive();

            MinecartSharp.GetServer().RemoveClient(client);

            if (client.State == ConnectionState.Play)
            {
                MinecartSharp.GetServer().BroadcastMessage(client.Username + " left the game");
                Logger.Log(LogType.Info, client.Username + " left the game");
            }

            tcpClient.Close();
            tcpClient.Dispose();
        }
    }
}
