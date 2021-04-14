using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices
{
    public class SignalRService : ISignalRService
    {
        public event Action<int> NotifyOfUpdate;
        public event Action<int> NotifyOfAdded;

        HubConnection connection;
        ILoggerProvider logger;

        /// <summary>
        /// Creates a connection and sets on what it needs to react.
        /// </summary>
        /// <param name="navManager"> Navigation manager used in the hubconnection. </param>
        public SignalRService(NavigationManager navManager, ILoggerProvider logger)
        {
            this.logger = logger;

            connection = new HubConnectionBuilder()
                    .WithUrl(navManager.ToAbsoluteUri(StringCollection.HubUri))
                    .WithAutomaticReconnect()
                    .Build();

            connection.On<int>(StringCollection.VisitorUpdatedString, (visitorId) =>
            {
                if (NotifyOfUpdate != null)
                {
                    NotifyOfUpdate?.Invoke(visitorId);
                }
            });

            connection.On<int>(StringCollection.VisitorAddedString, (visitorId) =>
            {
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
        public async Task SendUpdatedVisitorNotification(int visitorId) => 
            await connection.SendAsync(StringCollection.SendUpdatedVisitorNotificationString, visitorId);

        /// <summary>
        /// Sends an notification that someone is added to the SignalRHub with an Id.
        /// </summary>
        /// <param name="visitorId">Id of the added visitor.</param>
        /// <returns></returns>
        public async Task SendAddedVisitorNotification(int visitorId) => 
            await connection.SendAsync(StringCollection.SendAddedVisitorNotificationString, visitorId);

        /// <summary>
        /// Checks if the SignalR connection is still connected.
        /// </summary>
        public Task<bool> IsConnected() => 
            Task.FromResult(connection.State == HubConnectionState.Connected);
    }
}

