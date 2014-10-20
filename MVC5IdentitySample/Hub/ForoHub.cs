using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MVC5IdentitySample
{
    public class ForoHub : Hub
    {
        public void SendMessage(string topic)
        {
            Clients.All.addMessage(topic);
        }
    }
}