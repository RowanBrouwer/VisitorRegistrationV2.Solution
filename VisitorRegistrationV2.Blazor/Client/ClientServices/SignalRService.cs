using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;
using VisitorRegistrationV2.Blazor.Shared;
using VisitorRegistrationV2.Blazor.Client.PageModels;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices
{
    public class SignalRService
    {
        public event Action<int> NotifyOfUpdate;
        public event Action<int> NotifyOfAdded;
        public int receivedVisitorId;
        HubConnection connection;

        public SignalRService(NavigationManager navManager)
        {
            connection = new HubConnectionBuilder()
                    .WithUrl(navManager.ToAbsoluteUri("/visitorhub"))
                    .WithAutomaticReconnect()
                    .Build();


            connection.On<int>("ReceiveUpdateNotification", (visitorId) =>
            {
                receivedVisitorId = visitorId;
                if (NotifyOfUpdate != null)
                {
                    NotifyOfUpdate?.Invoke(visitorId);
                }
            });

            connection.On<int>("ReceiveAddedUserNotification", (visitorId) =>
            {
                receivedVisitorId = visitorId;
                if (NotifyOfAdded != null)
                {
                    NotifyOfAdded?.Invoke(visitorId);
                }
            });

            connection.StartAsync();
        }
        public async Task SendUpdateNotification(int visitorId) => await connection.SendAsync("SendUpdateNotification", visitorId);
        public async Task SendAddNotification(int visitorId) => await connection.SendAsync("SendAddNotification", visitorId);
        public bool IsConnected => connection.State == HubConnectionState.Connected;
    }
}

