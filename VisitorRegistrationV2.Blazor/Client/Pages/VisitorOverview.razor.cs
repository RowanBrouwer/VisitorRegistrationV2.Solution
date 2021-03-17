using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Timers;
using VisitorRegistrationV2.Blazor.Client.PageModels;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Blazor.Client.Pages
{
    public class VisitorOverviewModel : VisitorOverviewBaseModel
    {
        private Timer _delayTimer;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                visitors = await Http.GetFromJsonAsync<Visitor[]>("api/Visitor");
                
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(4, "input");
            builder.AddAttribute(7, "value", BindConverter.FormatValue(SearchTerm));
            builder.AddAttribute(8, "oninput", EventCallback.Factory.CreateBinder(this, __value => SearchTerm = __value, SearchTerm));
        }

        protected async Task VisitorArrived(Visitor visitorThatArrived, bool overRide)
        {
            showDialogArrived = false;
            if (visitorThatArrived.ArrivalTime == null || overRide == true)
            {
                visitorThatArrived.ArrivalTime = DateTime.Now;
                var response = await Http.PutAsJsonAsync($"api/visitor/{visitorThatArrived.Id}", visitorThatArrived);
                showDialogArrived = false;

                message = responseManager.GetMessage(response);
                await delayMessageReset();
            }
            else
            {
                showDialogArrived = true;
            }
        }
        protected async Task VisitorDeparted(Visitor visitorThatDeparted, bool overRide)
        {
            showDialogDeparted = false;
            if (visitorThatDeparted.DepartureTime == null || overRide == true)
            {
                visitorThatDeparted.DepartureTime = DateTime.Now;
                using var response = await Http.PutAsJsonAsync($"api/visitor/{visitorThatDeparted.Id}", visitorThatDeparted);
                {
                    showDialogDeparted = false;

                    message = responseManager.GetMessage(response);
                    await delayMessageReset();
                }
            }
            else
            {
                showDialogDeparted = true;
            }
        }

        protected void MessageDisposal()
        {
            message = null;
            StateHasChanged(); 
        }

        protected Task delayMessageReset()
        {
            _delayTimer = new System.Timers.Timer();
            _delayTimer.Interval = 3000;
            _delayTimer.Elapsed += (o, e) => MessageDisposal();
            _delayTimer.AutoReset = false;
            _delayTimer.Start();
            return Task.CompletedTask;
        }
    }

}

