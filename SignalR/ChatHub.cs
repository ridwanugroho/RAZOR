using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;


namespace belajarRazor.SignalR
{
    public class ChatHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections = 
            new ConnectionMapping<string>();

        public void SendChatMessage(string dari, string to, string message)
        {
            Console.WriteLine("dari {0}", dari);
            Console.WriteLine("tujuan {0}", to);
            Console.WriteLine("pesan {0}", message);


            foreach (var connectionId in _connections.GetConnections(to))
            {
                Console.WriteLine("con ID penerima : {0}", connectionId);
                
                Clients.Client(connectionId).SendAsync("GotMessage", $"{dari} : {message}");
            }
        }

        public override Task OnConnectedAsync()
        {
            string name = Context.UserIdentifier;
            string conID = Context.ConnectionId;
            Console.WriteLine("name : {0}", name);
            Console.WriteLine("con ID : {0}", conID);

            return base.OnConnectedAsync();
        }

        public void removeConId(string userId)
        {
            _connections.Remove(userId, Context.ConnectionId);
        }

        public string getConID()
        {
            return Context.ConnectionId;
        }

        public void bindConId(string UserId)
        {
            if (!_connections.GetConnections(UserId).Contains(Context.ConnectionId))
                _connections.Add(UserId, Context.ConnectionId);
        }

        public async Task SendMessage(string message, string from, string to)
        {
            Console.WriteLine(message);
            Console.WriteLine(from);
            Console.WriteLine(to);

            var msg = new 
            {
                from = from,
                to = to,
                message = message
            };

            await Clients.All.SendAsync("GotMessage", JsonConvert.SerializeObject(msg));
        }
        
    }
}