using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices
{
    public class SignalRService
    {
        public event Action NotifyOfUpdate;
        public event Action NotifyOfAdded;
        public int visitorId;
        HubConnection connection;

        public SignalRService(NavigationManager navManager)
        {
            connection = new HubConnectionBuilder()
                    .WithUrl(navManager.ToAbsoluteUri("/visitorhub"))
                    .Build();


            connection.On<int>("ReceiveUpdateNotification", (visitorId) =>
            {
                if (NotifyOfUpdate != null)
                {
                    NotifyOfUpdate?.Invoke();
                }

            });

            connection.On<int>("ReceiveAddedUser", (VisitorId) =>
            {
                if (NotifyOfAdded != null)
                {
                    NotifyOfAdded?.Invoke();
                }
            });

            connection.StartAsync();
        }
        public async Task SendUpdateNotification(int visitorId) => await connection.SendAsync("SendUpdateNotification", visitorId);

        public async Task SendAddNotification(int visitorId) => await connection.SendAsync("SendAddNotification", visitorId);
        public bool IsConnected => connection.State == HubConnectionState.Connected;
    }
}

