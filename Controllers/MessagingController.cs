using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using belajarRazor.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
// using PagedList;
using belajarRazor.Data;

namespace belajarRazor.Controllers
{
    public class MessagingController : Controller
    {
        private readonly ILogger<MessagingController> logger;
        private AppDbContex appDbContext;

        public MessagingController(ILogger<MessagingController> logger, AppDbContex appDbContex)
        {
            this.logger = logger;
            this.appDbContext = appDbContex;
        }

        public IActionResult GetReciever(string user)
        {
            logger.LogInformation("user to get information : {0}", user);

            var _user = (from u in appDbContext.User where u.email == user || u.username == user select u).First();

            return Ok(_user);
        }

        public IActionResult GetMsgHistory(int recId)
        {
            var userId = HttpContext.Session.GetInt32("id").GetValueOrDefault();
            var myMessages = (from m in appDbContext.Conversations.Include(u=>u.From).Include(u=>u.To) where 
                            (m.From.id == userId && m.To.id == recId) ||
                            (m.From.id == recId && m.To.id == userId) select m).OrderBy(t=>t.SentTime);
            
            // var setReadStatThread = new Thread(()=>setReadStatus(recId, myMessages.ToList()));
            // setReadStatThread.Start();
            setReadStatus(recId, myMessages.ToList());

            var _msg = new List<object>();

            foreach (var msg in myMessages)
            {
                int identifier = 0;
                bool readStat = false;

                if(msg.From.id == userId)
                    identifier = 1;
                
                if(msg.Read != null)
                    readStat = true;

                var temp = new
                {
                    identifier = identifier,
                    m = msg.Message,
                    time = msg.SentTime,
                    read = readStat
                };

                _msg.Add(temp);
            }

            return Ok(_msg);
        }

        public IActionResult SendMessage(int recId, string msg)
        {
            var userId = HttpContext.Session.GetInt32("id").GetValueOrDefault();

            var _msg = new Conversation()
            {
                From = appDbContext.User.Find(userId),
                To = appDbContext.User.Find(recId),
                Message = msg,
                SentTime = DateTime.Now,
            };

            appDbContext.Conversations.Add(_msg);
            appDbContext.SaveChanges();

            return Ok(_msg);
        }
    
        private void setReadStatus(int recId, List<Conversation> msgs)
        {
            foreach(var msg in msgs)
            {
                if(msg.From.id == recId)
                {
                    var temp = appDbContext.Conversations.Find(msg.uuid);
                    temp.Read = DateTime.Now;
                }
            }

            appDbContext.SaveChanges();
        }
    }
}