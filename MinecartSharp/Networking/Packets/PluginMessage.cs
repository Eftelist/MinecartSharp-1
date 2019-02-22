using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using MinecartSharp.Enums;
using MinecartSharp.Networking.Objects;

namespace MinecartSharp.Networking.Packets
{
    class PluginMessage : IPacket
    {
        public int PacketId => 0x19;

        public ConnectionState State => ConnectionState.Play;

        public void Read(Client client, Message message)
        {
            
        }

        public void Write(Client client, Message message, params object[] arguments)
        {
            message.WriteVarInt(0x19);
            message.WriteString((string)arguments[0]);
            message.WriteString((string)arguments[1]);
            message.FlushData();
        }
    }
}
