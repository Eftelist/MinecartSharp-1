using System;
using System.Collections.Generic;
using System.Text;
using MinecartSharp.Enums;
using MinecartSharp.Networking.Objects;

namespace MinecartSharp.Networking.Packets
{
    class KeepAlive : IPacket
    {
        public int PacketId => 0x21;

        public ConnectionState State => ConnectionState.Play;

        public void Read(Client client, Message message)
        {
            
        }

        public void Write(Client client, Message message, params object[] arguments)
        {
            message.WriteVarInt(0x21);

            var ms = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;

            message.WriteLong((long)ms);
            message.FlushData();
        }
    }
}
