using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineMusic.Hubs
{
    public class Chat : Hub
    {
        public void Time()
        {
            Clients.All.time(DateTime.Now);
        }
        public void Send(string name, string msg)
        {
            Clients.All.sendMsg(name, msg, DateTime.Now);

        }
    }
}