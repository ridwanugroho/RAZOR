using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using belajarRazor.Data;
using belajarRazor.Controllers;
using belajarRazor.Models;


namespace belajarRazor.SignalR
{
    public class ChatHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();

        private readonly ILogger<MessagingController> logger;
        private AppDbContex appDbContext;

        public ChatHub(ILogger<MessagingController> logger, AppDbContex appDbContex)
        {
            this.logger = logger;
            this.appDbContext = appDbContex;
        }

        public void SendChatMessage(string from, string to, string message)
        {
            foreach (var connectionId in _connections.GetConnections(to))
                Clients.Client(connectionId).SendAsync("GotMessage", message);

            var _msg = new Conversation()
            {
                From = appDbContext.User.Find(int.Parse(from)),
                To = appDbContext.User.Find(int.Parse(to)),
                Message = message,
                SentTime = DateTime.Now,
            };

            appDbContext.Conversations.Add(_msg);
            appDbContext.SaveChanges();
        }

        public override Task OnConnectedAsync()
        {
            string name = Context.UserIdentifier;
            string conID = Context.ConnectionId;
            logger.LogInformation("name : {0}", name);
            logger.LogInformation("con ID : {0}", conID);

            return base.OnConnectedAsync();
        }

        public void removeConId(string userId)
        {
            _connections.Remove(userId, Context.ConnectionId);
        }

        public void bindConId(string UserId)
        {
            if (!_connections.GetConnections(UserId).Contains(Context.ConnectionId))
                _connections.Add(UserId, Context.ConnectionId);
        }
    }
}