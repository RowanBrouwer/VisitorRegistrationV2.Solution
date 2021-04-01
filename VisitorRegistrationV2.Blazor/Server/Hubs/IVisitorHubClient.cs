using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorRegistrationV2.Blazor.Server.Hubs
{
    public interface IVisitorHubClient
    {
        public Task SendVisitorUpdatedNotification(string message, int visitorId);
        public Task SendVisitorAddedNotification(string message, int visitorId);
    }
}
