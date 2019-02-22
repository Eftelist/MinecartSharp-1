using System;
using System.Collections.Generic;
using System.Text;
using MinecartSharp.Enums;
using MinecartSharp.Math;
using MinecartSharp.Networking.Objects;

namespace MinecartSharp.Networking.Packets
{
    class SpawnPosition : IPacket
    {
        public int PacketId => 0x49;

        public ConnectionState State => ConnectionState.Play;

        public void Read(Client client, Message message)
        {
            
        }

        public void Write(Client client, Message message, params object[] arguments)
        {
            message.WriteVarInt(0x49);

            var vec3 = new Vector3(1f, 1f, 1f);

            message.WriteLong(vec3.ToLocation());
            message.FlushData();

            PacketHandler.GetPacket(0x32, ConnectionState.Play).Write(client, message);
        }
    }
}
