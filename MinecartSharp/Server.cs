using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MinecartSharp.Enums;
using MinecartSharp.Networking;
using MinecartSharp.Networking.Objects;
using MinecartSharp.Objects;

namespace MinecartSharp
{
    class Server
    {
        private Config _config;
        private readonly Listener _listener;

        private readonly List<Client> _clients = new List<Client>();

        public Server(Config config)
        {
            _config = config;

            _listener = new Listener(_config.Port, _config);
        }

        public void Start()
        {
            _listener.Start();
        }

        public void Stop()
        {
            _listener.Stop();
        }

        public List<Client> GetClients()
        {
            return _clients;
        }

        public List<Client> GetPlayers()
        {
            // TODO: Return an player object
            return _clients.Where(x => x.State == ConnectionState.Play).ToList();
        }

        public void BroadcastMessage(string message)
        {
            GetPlayers().ForEach(x => x.SendMessage(message));
        }

        public void AddClient(Client client)
        {
            _clients.Add(client);
        }

        public void RemoveClient(Client client)
        {
            _clients.Remove(client);
        }
    }
}
