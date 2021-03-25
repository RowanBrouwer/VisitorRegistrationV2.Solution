using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices.ISignalRCommandsFolder
{
    public interface ISignalRCommands
    {
        public bool IsConnected();
        public Task StartHubConnection();
        public Task SendUpdatedUserNotification(int visitorId);
        public Task SendAddedUserNotification(int visitorId);
    }
}
