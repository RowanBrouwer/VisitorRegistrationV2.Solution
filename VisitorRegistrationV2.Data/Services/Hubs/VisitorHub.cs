using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorRegistrationV2.Data.Services.Hubs
{
    public class VisitorHub : Hub
    {
        public async Task SendUpdateNotification(string message)
        {
            if (message == "UpdateAll")
            {
                await Clients.All.SendAsync("ReceiveMessage", message);
            }
            if (message == "UpdatedSingle")
            {
                await Clients.All.SendAsync("ReceiveMessage", message);
            }
        }
    }
}
