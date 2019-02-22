using System;
using System.Collections.Generic;
using System.Text;
using MinecartSharp.Enums;
using MinecartSharp.Networking.Objects;

namespace MinecartSharp.Networking.Packets
{
    class PlayerPosition : IPacket
    {
        public int PacketId => 0x32;

        public ConnectionState State => ConnectionState.Play;

        public void Read(Client client, Message message)
        {
        }

        public void Write(Client client, Message message, params object[] arguments)
        {
            message.WriteVarInt(0x32);

            message.WriteDouble(0.0);
            message.WriteDouble(0.0);
            message.WriteDouble(0.0);

            message.WriteFloat(90);
            message.WriteFloat(180);

            message.WriteByte((byte)0);
            message.WriteVarInt(1);

            message.FlushData();
        }
    }
}
