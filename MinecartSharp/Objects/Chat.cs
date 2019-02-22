using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MinecartSharp.Objects
{
    class Chat
    {
        [JsonProperty("text")] public string Text;

        [JsonProperty("bold")] public bool Bold;
        [JsonProperty("obfuscated")] public bool Obfuscated;
        [JsonProperty("strikethrough")] public bool Strikethrough;
        [JsonProperty("underlineD")] public bool Underline;
        [JsonProperty("italic")] public bool Italic;

        [JsonProperty("color")] public string Color;
    }
}
