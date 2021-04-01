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

        /// <summary>
        /// Creates a connection and sets on what it needs to react.
        /// </summary>
        /// <param name="navManager"> Navigation manager used in the hubconnection. </param>
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


        /// <summary>
        /// Sends an notification that someone is updated to the SignalRHub with an Id.
        /// </summary>
        /// <param name="visitorId"> Id of the updated visitor.</param>
        /// <returns></returns>
        public async Task SendUpdateNotification(int visitorId) => await connection.SendAsync("SendUpdateNotification", visitorId);

        /// <summary>
        /// Sends an notification that someone is added to the SignalRHub with an Id.
        /// </summary>
        /// <param name="visitorId">Id of the added visitor.</param>
        /// <returns></returns>
        public async Task SendAddNotification(int visitorId) => await connection.SendAsync("SendAddNotification", visitorId);

        /// <summary>
        /// Checks if the SignalR connection is still connected.
        /// </summary>
        public bool IsConnected => connection.State == HubConnectionState.Connected;
    }
}

