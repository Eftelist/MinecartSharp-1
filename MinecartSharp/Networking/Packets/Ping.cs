using System;
using System.Collections.Generic;
using System.Text;
using MinecartSharp.Enums;
using MinecartSharp.Networking.Objects;

namespace MinecartSharp.Networking.Packets
{
    class Ping : IPacket
    {
        public int PacketId => 0x01;

        public ConnectionState State => ConnectionState.Status;

        public void Read(Client client, Message message)
        {
            Write(client, message);
        }

        public void Write(Client client, Message message, params object[] arguments)
        {
            // send same packet back
            client.SendData(message.Data);
        }
    }
}
