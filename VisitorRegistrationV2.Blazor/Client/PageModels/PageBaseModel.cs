using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using VisitorRegistrationV2.Blazor.Client.ClientServices;
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
        [Inject]
        public VisitorService ClientService { get; set; }
        protected string Message { get; set; }

        public event Action ResetMessage;

        private Timer _delayTimer;
        protected void MessageDisposal()
        {
            Message = null;
            StateHasChanged();
        }

        protected async Task delayMessageReset()
        {
            _delayTimer = new Timer();
            _delayTimer.Interval = 3000;
            _delayTimer.Elapsed += (o, e) => ResetMessage.Invoke();
            _delayTimer.AutoReset = false;
            _delayTimer.Stop();
            _delayTimer.Start();
            await Task.CompletedTask;
        }
    }
}
