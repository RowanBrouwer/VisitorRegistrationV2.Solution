using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using VisitorRegistrationV2.Blazor.Client.ClientServices;
using VisitorRegistrationV2.Blazor.Client.ClientServices.HttpService;
using VisitorRegistrationV2.Blazor.Client.ClientServices.IMessageResponse;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Blazor.Client.PageModels
{
    public class PageBaseModel : ComponentBase
    {
        [Inject]
        protected IHttpService Http { get; set; }
        [Inject]
        protected NavigationManager NavManager { get; set; }
        [Inject]
        protected IMessageResponse ResponseManager { get; set; }
        [Inject]
        protected SignalRService SignalRService { get; set; }
        protected string Message { get; set; }

        private Timer _delayTimer;
        protected Task MessageDisposal()
        {
            Message = null;
            StateHasChanged();
            return Task.CompletedTask;
        }

        protected async Task delayMessageReset()
        {
            _delayTimer = new Timer();
            _delayTimer.Interval = 3000;
            _delayTimer.Elapsed += (o, e) => MessageDisposal();
            _delayTimer.AutoReset = false;
            _delayTimer.Stop();
            _delayTimer.Start();
            await Task.CompletedTask;
        }
    }
}
