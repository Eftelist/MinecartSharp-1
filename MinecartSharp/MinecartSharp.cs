using System;
using System.IO;
using MinecartSharp.Utils;
using Newtonsoft.Json;

namespace MinecartSharp
{
    class MinecartSharp
    {
        private static Config _config;
        private static Server _server;

        // Get the current directory which the server is run from
        private static readonly string Location = AppContext.BaseDirectory;

        private static readonly string ConfigFile = Location + Path.DirectorySeparatorChar + "config.json";
        private static readonly string FaviconFile = Location + Path.DirectorySeparatorChar + "server-icon.png";

        static void Main(string[] args)
        {
            Console.Title = "MinecartSharp server";
            Console.WriteLine("Starting MinecartSharp...");

#if DEBUG
            Logger.Debug = true;
#endif

            Logger.Log(LogType.Info, "Loading server configuration");

            // check if config already exist
            if (!File.Exists(ConfigFile))
            {
                // create default config
                using (StreamWriter file = File.CreateText(ConfigFile))
                {
                    file.Write(JsonConvert.SerializeObject(new Config(), Formatting.Indented));
                }
            }

            _config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(ConfigFile));

            if (Logger.Debug == false)
            {
                Logger.Debug = _config.DebugMode;
            }

            if (File.Exists(FaviconFile))
            {
                var servericon = File.ReadAllBytes(FaviconFile);
                _config.Favicon = "data:image/png;base64," + Convert.ToBase64String(servericon);
            }

            // start a new server instance
            _server = new Server(_config);
            _server.Start();
        }

        public static Config GetConfig()
        {
            return _config;
        }

        public static Server GetServer()
        {
            return _server;
        }
    }
}
