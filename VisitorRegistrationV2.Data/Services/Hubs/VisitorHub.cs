﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Data.Services.Hubs
{
    public class VisitorHub : Hub
    {
        public async Task SendUpdateNotification(int visitorId)
        {
            await Clients.All.SendAsync("ReceiveUpdateNotification", visitorId);
        }

        public async Task SendAddNotification(int VisitorId)
        {
            await Clients.All.SendAsync("ReceiveAddedUserNotification", VisitorId);
        }
    }
}
