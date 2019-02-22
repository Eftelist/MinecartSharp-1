using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using MinecartSharp.Utils;
using MinecartSharp.Enums;
using MinecartSharp.Networking.Objects;

namespace MinecartSharp.Networking.Packets
{
    class Login : IPacket
    {
        public int PacketId => 0x00;

        public ConnectionState State => ConnectionState.Login;

        public void Read(Client client, Message message)
        {
            var username = message.ReadUsername();

            client.Username = username;

            var address = ((IPEndPoint) client.TcpClient.Client.RemoteEndPoint).Address;
            Logger.Log(LogType.Info, $"{client.Username}[{address}] logged in");

            // skip encryption and compression for now
            Write(client, message);
        }

        public void Write(Client client, Message message, params object[] arguments)
        {
            message.WriteVarInt(0x02);
            message.WriteString(Guid.NewGuid().ToString("").ToUpper());
            message.WriteString(client.Username);
            message.FlushData();

            // set state to play
            client.State = ConnectionState.Play;
            client.StartKeepAlive();

            // write mc:brand plugin packet
            // TODO: Actually make this packet work should probably be an byte array
            new PluginMessage().Write(client, message, "minecraft:brand", "MinecartSharp");

            // also write join game packet
            PacketHandler.GetPacket(0x25, client.State).Write(client, message);
        }
    }
}
