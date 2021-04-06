using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Blazor.Server.Hubs
{
    public class VisitorHub : Hub
    {
        /// <summary>
        /// Sends update notification to all clients.
        /// </summary>
        /// <param name="visitorId">Id of visitor that got updated.</param>
        /// <returns></returns>
        public async Task SendUpdateNotification(int visitorId)
        {
            await Clients.All.SendAsync(StringCollection.VisitorUpdatedString, visitorId);
        }

        /// <summary>
        /// Sends added notification to all clients.
        /// </summary>
        /// <param name="VisitorId">Id of visitor that got added.</param>
        /// <returns></returns>
        public async Task SendAddNotification(int VisitorId)
        {
            await Clients.All.SendAsync(StringCollection.VisitorAddedString, VisitorId);
        }
    }
}
