using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using VisitorRegistrationV2.Blazor.Client.ClientServices;
using VisitorRegistrationV2.Blazor.Client.PageModels;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Blazor.Client.Pages
{
    public class VisitorOverviewModel : VisitorOverviewBaseModel
    {
        /// <summary>
        /// Used as a temporary holding field for a visitor.
        /// </summary>
        protected Visitor SelectedVisitor { get; set; }

        private event Action Changed;

        protected override async Task OnInitializedAsync()
        {
            SignalRService.NotifyOfUpdate += OnNotifyOfUpdate;
            SignalRService.NotifyOfAdded += OnNotifyOfAdded;
            Changed += OnNotifyOfChange;
            ResetMessage += MessageDisposal;
            ResetMessage += ClientService.MessageDisposal;

            await LoadData();
        }

        /// <summary>
        /// Invokes StateHasChanged async
        /// </summary>
        public void OnNotifyOfChange()
        {
            InvokeAsync(() => StateHasChanged());
        }

        /// <summary>
        /// Handler for NotifyOfUpdate, Runs an async task.
        /// </summary>
        /// <param name="visitorId">Id of updated visitor.</param>
        public void OnNotifyOfUpdate(int visitorId)
        {
            Task.Run(async () => await GetUpdatedUser(visitorId));
        }

        /// <summary>
        /// Handler for NotifyOfAdded, Runs an async task.
        /// </summary>
        /// <param name="visitorId">Id of Added visitor received from SignalR.</param>
        public void OnNotifyOfAdded(int visitorId)
        {
            Task.Run(async () => await GetAddedUserAndAddToList(visitorId));
        }

        /// <summary>
        /// Requests the visitor through the IhttpService and adds it to the list visitors.
        /// </summary>
        /// <param name="visitorId">Id of Added visitor received from SignalR.</param>
        /// <returns></returns>
        protected async Task GetAddedUserAndAddToList(int visitorId)
        {
            try
            { 
                var newvisitor = await Http.GetVisitor(visitorId);

                visitors.Add(newvisitor);

                Changed.Invoke();
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }

        /// <summary>
        /// Requests the visitor through the IhttpService by id and adds it to the list visitors.
        /// </summary>
        /// <param name="visitorId">Id of updated visitor received from SignalR.</param>
        /// <returns></returns>
        protected async Task GetUpdatedUser(int visitorId)
        {
            try
            {
                var FoundVisitor = await Http.GetVisitor(visitorId);
                await SetUpdatedUser(FoundVisitor);   
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }

        public Task SetUpdatedUser(Visitor visitor)
        {
            int index = visitors.FindIndex(v => v.Id == visitor.Id);

            if (index >= 0)
                visitors[index] = visitor;
            Changed.Invoke();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Requests a list of visitors through the IHttpService.
        /// </summary>
        /// <returns></returns>
        protected async Task LoadData()
        {
            try
            {
                visitors = await Http.GetVisitorList();
                Changed.Invoke();
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement
                (6, "input");

            builder.AddAttribute
                (7, "value", BindConverter.FormatValue(SearchTerm));

            builder.AddAttribute
                (8, "oninput", EventCallback.Factory.CreateBinder
                (this, __value => SearchTerm = __value, SearchTerm));
        }

        /// <summary>
        /// Checks if it can write or override the ArrivalTime.
        /// </summary>
        /// <param name="visitorThatArrived">Visitor wich got selected to have their ArrivalTime updated.</param>
        /// <param name="overRide">weither or not to override this visitors ArrivalTime.</param>
        /// <returns></returns>
        protected async Task VisitorArrived(Visitor visitorThatArrived, bool overRide)
        {
            SelectedVisitor = null;
            showDialogArrived = false;
            if (visitorThatArrived.ArrivalTime == null || overRide == true)
            {
                visitorThatArrived = await ClientService.VisitorArrives(visitorThatArrived, overRide, null);
                await SetUpdatedUser(visitorThatArrived);
                Message = ClientService.Message; 
                await delayMessageReset();
                Changed.Invoke();
            }
            else
            {
                SelectedVisitor = visitorThatArrived;
                showDialogArrived = true;
            }
        }

        /// <summary>
        /// Checks if it can write or override the DepartureTime.
        /// </summary>
        /// <param name="visitorThatDeparted">Visitor wich got selected to have their DepartureTime updated.</param>
        /// <param name="overRide">weither or not to override this visitors DepartureTime.</param>
        /// <returns></returns>
        protected async Task VisitorDeparted(Visitor visitorThatDeparted, bool overRide)
        {
            SelectedVisitor = null;
            showDialogDeparted = false;
            if (visitorThatDeparted.DepartureTime == null || overRide == true)
            {
                visitorThatDeparted = await ClientService.VisitorDeparts(visitorThatDeparted, overRide, null);
                await SetUpdatedUser(visitorThatDeparted);
                Message = ClientService.Message;
                await delayMessageReset();
                Changed.Invoke();
            }
            else
            {
                SelectedVisitor = visitorThatDeparted;
                showDialogDeparted = true;
            }
        }

        protected void RedirectToCreatePage()
        {
            NavManager.NavigateTo("/Create");
        }

        protected void Dispose()
        {
            SignalRService.NotifyOfUpdate -= OnNotifyOfUpdate;
            SignalRService.NotifyOfAdded -= OnNotifyOfAdded;
            Changed -= OnNotifyOfChange;
            ResetMessage -= MessageDisposal;
            ResetMessage -= ClientService.MessageDisposal;
        }
    }
}

