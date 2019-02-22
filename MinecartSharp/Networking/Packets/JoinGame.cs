using System;
using System.Collections.Generic;
using System.Text;
using MinecartSharp.Enums;
using MinecartSharp.Networking.Objects;

namespace MinecartSharp.Networking.Packets
{
    class JoinGame : IPacket
    {
        public int PacketId => 0x25;

        public ConnectionState State => ConnectionState.Play;

        public void Read(Client client, Message message)
        {
            
        }

        public void Write(Client client, Message message, params object[] arguments)
        {
            message.WriteVarInt(0x25);
            // TODO: give client/player unique entity id
            message.WriteInt(1);
            message.WriteByte((byte)2); // Gamemode
            message.WriteInt(0); // Dimension
            message.WriteByte((byte)0); // Difficulty
            message.WriteByte((byte)0); // Max players, ignored by client
            message.WriteString("default"); // Level type
            message.WriteBool(true); // Debug

            message.FlushData();

            PacketHandler.GetPacket(0x49, ConnectionState.Play).Write(client, message);
        }
    }
}
