using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.SignalR.Client;
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
    public class VisitorOverviewModel : VisitorOverviewPageModel
    {
        protected Visitor SelectedVisitor { get; set; }

        protected override async Task OnInitializedAsync()
        {
            hubConnection.On<int>("UpdateNotification", async (visitorId) =>
            {
                var visitorToUpdate = await crudCommands.GetUpdatedUser(visitorId);
                var foundVisitor = visitors.First(v => v.Id == visitorId);
                foundVisitor = visitorToUpdate;
                StateHasChanged();
            });

            hubConnection.On<int>("ReceiveAddedUser",async (VisitorId) =>
            {
                var visitorToAdd = await crudCommands.GetUpdatedUser(VisitorId);
                visitors.Add(visitorToAdd);
                StateHasChanged();
            });

            await SignalRCommands.StartHubConnection();

            visitors = await crudCommands.LoadVisitorOverViewItems();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(4, "input");
            builder.AddAttribute(7, "value", BindConverter.FormatValue(SearchTerm));
            builder.AddAttribute(8, "oninput", EventCallback.Factory.CreateBinder(this, __value => SearchTerm = __value, SearchTerm));
        }
        
        protected async Task VisitorArrived(Visitor visitorThatArrived, bool overRide)
        {
            SelectedVisitor = null;
            showDialogArrived = false;
            if (visitorThatArrived.ArrivalTime == null || overRide == true)
            {
                visitorThatArrived.ArrivalTime = DateTime.Now;
                visitorThatArrived.DepartureTime = null;
                showDialogArrived = false;
                Message = await crudCommands.VisitorArrived(visitorThatArrived);
            }
            else
            {
                SelectedVisitor = visitorThatArrived;
                showDialogArrived = true;
            }
        }
        protected async Task VisitorDeparted(Visitor visitorThatDeparted, bool overRide)
        {
            SelectedVisitor = null;
            showDialogDeparted = false;
            if (visitorThatDeparted.DepartureTime == null || overRide == true)
            {
                visitorThatDeparted.DepartureTime = DateTime.Now;
                showDialogArrived = false;
                Message = await crudCommands.VisitorDeparted(visitorThatDeparted);
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
    }
}

