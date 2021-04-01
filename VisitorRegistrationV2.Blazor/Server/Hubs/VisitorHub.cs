using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Client.ClientServices;

namespace VisitorRegistrationV2.Blazor.Server.Hubs
{
    public class VisitorHub : Hub<IClientNotifyHub>
    {
        /// <summary>
        /// Sends update notification to all clients.
        /// </summary>
        /// <param name="visitorId">Id of visitor that got updated.</param>
        /// <returns></returns>
        public async Task ReceiveUpdatedVisitorNotification(int visitorId)
        {
            await Clients.All.UpdatedVisitorNotify(visitorId);
        }

        /// <summary>
        /// Sends added notification to all clients.
        /// </summary>
        /// <param name="VisitorId">Id of visitor that got added.</param>
        /// <returns></returns>
        public async Task ReceiveAddedVisitorNotification(int visitorId)
        {
            await Clients.All.AddedVisitorNotify(visitorId);
        }
    }
}
