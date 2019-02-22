using System;
using System.Collections.Generic;
using System.Text;
using MinecartSharp.Enums;
using MinecartSharp.Networking.Objects;

namespace MinecartSharp.Networking.Packets
{
    interface IPacket
    {
        int PacketId { get; }

        ConnectionState State { get; }

        void Read(Client client, Message message);

        void Write(Client client, Message message, params object[] arguments);
    }
}
