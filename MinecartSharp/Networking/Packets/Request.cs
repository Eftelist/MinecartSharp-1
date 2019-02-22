using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MinecartSharp.Enums;
using MinecartSharp.Networking.Objects;
using MinecartSharp.Objects;
using Newtonsoft.Json;

namespace MinecartSharp.Networking.Packets
{
    class Request : IPacket
    {
        public int PacketId => 0x00;

        public ConnectionState State => ConnectionState.Status;

        public void Read(Client client, Message message)
        {
            // client sends no fields in this packet
            Write(client, message);
        }

        public void Write(Client client, Message message, params object[] arguments)
        {
            message.WriteVarInt(0x00);

            ServerListping serverlistping = new ServerListping()
            {
                Version = new ServerpingVersion()
                {
                    Name = "MinecartSharp 1.13.2",
                    Protocol = 404
                },
                Players = new ServerpingPlayers()
                {
                    Max = MinecartSharp.GetConfig().MaxPlayers,
                    Online = MinecartSharp.GetServer().GetPlayers().Count,
                    Players = MinecartSharp.GetServer().GetPlayers().Select(x => new ServerpingPlayer(){ Name = x.Username }).ToList()
                },
                Description = new ServerpingDescription()
                {
                    Motd = MinecartSharp.GetConfig().Motd
                },
                // TODO: make favicon work
                Favicon = ""//MinecartSharp.GetConfig().Favicon
            };

            message.WriteString(JsonConvert.SerializeObject(serverlistping));
            message.FlushData();
        }
    }
}
