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
using VisitorRegistrationV2.Blazor.Client.ClientServices;
using VisitorRegistrationV2.Blazor.Client.PageModels;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Blazor.Client.Pages
{
    public class VisitorOverviewModel : VisitorOverviewBaseModel
    {
        protected Visitor SelectedVisitor { get; set; }

        protected override async Task OnInitializedAsync()
        {
            SignalRService.NotifyOfUpdate += onNotifyOfUpdate;
            SignalRService.NotifyOfAdded += onNotifyOfAdded; 

            await LoadData();
        }

        protected async Task Refresh()
        {
            await InvokeAsync(StateHasChanged);
        }

        public void onNotifyOfUpdate(int visitorId)
        {
            Task.Run(async () => await GetUpdatedUser(visitorId));
            StateHasChanged();
        }
        public void onNotifyOfAdded(int visitorId)
        {
            Task.Run(async () => await GetAddedUserAndAddToList(visitorId));
            StateHasChanged();
        }

        protected async Task GetAddedUserAndAddToList(int visitorId)
        {
            try
            { 
                var newvisitor = await Http.GetFromJsonAsync<Visitor>($"api/Visitor/{visitorId}");
                visitors.Add(newvisitor);
                StateHasChanged();
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            };
        }

        protected async Task GetUpdatedUser(int visitorId)
        {
            try
            {
                var FoundVisitor = visitors.First(v => v.Id == visitorId);
                FoundVisitor = await Http.GetFromJsonAsync<Visitor>($"api/Visitor/{visitorId}");
                base.StateHasChanged();
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }

        protected async Task LoadData()
        {
            try
            {
                visitors = await Http.GetFromJsonAsync<List<Visitor>>("api/Visitor");
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(6, "input");
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

               using var response = await Http.PutAsJsonAsync($"api/visitor/{visitorThatArrived.Id}", visitorThatArrived);
               {
                    showDialogArrived = false;

                    if (SignalRService.IsConnected)
                    {
                        Message = ResponseManager.GetMessage(response);

                        await SignalRService.SendUpdateNotification(visitorThatArrived.Id);
                        await delayMessageReset();
                    }
                    else
                    {
                        Message = "No Connection";
                    }
                }
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
                using var response = await Http.PutAsJsonAsync($"api/visitor/{visitorThatDeparted.Id}", visitorThatDeparted);
                {
                    showDialogDeparted = false;

                    if (SignalRService.IsConnected)
                    {
                        Message = ResponseManager.GetMessage(response);

                        await SignalRService.SendUpdateNotification(visitorThatDeparted.Id);
                        await delayMessageReset();
                    }
                    else
                    {
                        Message = "No Connection";
                    } 
                }
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

