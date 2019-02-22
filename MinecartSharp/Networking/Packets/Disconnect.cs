using System;
using System.Collections.Generic;
using System.Text;
using MinecartSharp.Enums;
using MinecartSharp.Networking.Objects;
using MinecartSharp.Objects;
using Newtonsoft.Json;

namespace MinecartSharp.Networking.Packets
{
    class Disconnect : IPacket
    {
        public int PacketId => 0x1A;

        public ConnectionState State => ConnectionState.Play;

        public void Read(Client client, Message message)
        {
            
        }

        public void Write(Client client, Message message, params object[] arguments)
        {
            switch (client.State)
            {
                case ConnectionState.Play:
                    message.WriteVarInt(0x1A);

                    break;
                case ConnectionState.Login:
                    message.WriteVarInt(0x00);

                    break;
            }

            message.WriteString(JsonConvert.SerializeObject((Chat)arguments[0]));
            message.FlushData();
        }
    }
}
