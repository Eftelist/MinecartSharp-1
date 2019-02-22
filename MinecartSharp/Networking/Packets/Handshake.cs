using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using MinecartSharp.Enums;
using MinecartSharp.Networking.Objects;

namespace MinecartSharp.Networking.Packets
{
    class Handshake : IPacket
    {
        public int PacketId => 0x00;

        public ConnectionState State => ConnectionState.Unknown;

        public void Read(Client client, Message message)
        {
            var version = message.ReadVarInt();
            var address = message.ReadString();
            var port = message.ReadShort();
            var state = message.ReadVarInt();

            // 1 | Status
            // 2 | Login
            switch (state)
            {
                case 1:
                    client.State = ConnectionState.Status;

                    PacketHandler.GetPacket(0x00, client.State).Read(client, message);
                    break;
                case 2:
                    client.State = ConnectionState.Login;

                    PacketHandler.GetPacket(0x00, client.State).Read(client, message);
                    break;
            }
        }

        public void Write(Client client, Message message, params object[] arguments)
        {
            
        }
    }
}
