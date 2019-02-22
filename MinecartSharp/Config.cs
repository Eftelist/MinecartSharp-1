using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MinecartSharp
{
    class Config
    {
        public int Port { get; set; } = 25565;
        public int MaxPlayers { get; set; } = 16;

        public string Motd { get; set; } = "§cMinecartSharp Server";

        public bool DebugMode { get; set; } = false;

        [JsonIgnore]
        public string Favicon { get; set; } = "";
    }
}
