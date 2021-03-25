using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Client.ClientServices.IHttpCommandsFolder;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices.ISignalRCommandsFolder
{
    public class SignalRCommands : ISignalRCommands
    {
        private HubConnection hubConnection;

        public SignalRCommands(HubConnection hubConnection)
        {
            this.hubConnection = hubConnection;
        }

        public bool IsConnected()
        {
            switch (hubConnection.State == HubConnectionState.Connected)
            {
                case true:
                    return true;
                default:
                    return false;
            }
        }

        public async Task SendAddedUserNotification(int visitorId)
        {
            await hubConnection.SendAsync("SendAddNotification");
        }

        public async Task SendUpdatedUserNotification(int visitorId)
        {
            await hubConnection.SendAsync("SendUpdateNotification");
        }

        public async Task StartHubConnection()
        {
            await hubConnection.StartAsync();
        }
    }
}
