using System;
using System.Collections.Generic;
using System.Text;
using MinecartSharp.Enums;
using MinecartSharp.Networking.Objects;
using MinecartSharp.Objects;
using MinecartSharp.Utils;
using Newtonsoft.Json;

namespace MinecartSharp.Networking.Packets
{
    class ChatMessage : IPacket
    {
        public int PacketId => 0x02;

        public ConnectionState State => ConnectionState.Play;

        public void Read(Client client, Message message)
        {
            var msg = message.ReadString();

            if (msg.StartsWith("/"))
            {
                // handle command

                return;
            }

            // if not command broadcast message
            MinecartSharp.GetServer().BroadcastMessage($"{client.Username}: {msg}");
            Logger.Log(LogType.Info, $"{client.Username}: {msg}");
        }

        public void Write(Client client, Message message, params object[] arguments)
        {
            message.WriteVarInt(0x0E);
            message.WriteString(JsonConvert.SerializeObject((Chat)arguments[0]));
            message.WriteByte((byte)0);
            message.FlushData();
        }
    }
}
