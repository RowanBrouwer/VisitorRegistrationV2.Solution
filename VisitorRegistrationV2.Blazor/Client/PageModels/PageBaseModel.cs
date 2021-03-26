using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using VisitorRegistrationV2.Blazor.Client.ClientServices.IMessageResponse;

namespace VisitorRegistrationV2.Blazor.Client.PageModels
{
    public class PageBaseModel : ComponentBase
    {
        [Inject]
        protected HttpClient Http { get; set; }
        [Inject]
        protected NavigationManager NavManager { get; set; }
        [Inject]
        protected IMessageResponse ResponseManager { get; set; }
        [Inject]
        protected HubConnection hubConnection { get; set; }
        protected string Message { get; set; }

        private Timer _delayTimer;
        protected bool IsConnected =>
        hubConnection.State == HubConnectionState.Connected;

        protected void MessageDisposal()
        {
            Message = null;
            StateHasChanged();
        }

        protected Task delayMessageReset()
        {
            _delayTimer = new Timer();
            _delayTimer.Interval = 3000;
            _delayTimer.Elapsed += (o, e) => MessageDisposal();
            _delayTimer.AutoReset = false;
            _delayTimer.Start();
            return Task.CompletedTask;
        }
    }
}
