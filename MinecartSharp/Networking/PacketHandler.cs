using System;
using System.Collections.Generic;
using System.Linq;
using MinecartSharp.Enums;
using MinecartSharp.Networking.Packets;

namespace MinecartSharp.Networking
{
    class PacketHandler
    {
        private static readonly List<IPacket> Packets = new List<IPacket>()
        {
            new Handshake(),
            new Request(),
            new Ping(),
            new Login(),
            new JoinGame(),
            new PlayerPosition(),
            new SpawnPosition(),
            new Disconnect()
        };

        public static IPacket GetPacket(int id, ConnectionState state)
        {
            return Packets.Find(x => x.PacketId == id && x.State == state);
        }

        public static bool Exists(int id, ConnectionState state)
        {
            return Packets.Any(x => x.PacketId == id && x.State == state);
        }
    }
}
