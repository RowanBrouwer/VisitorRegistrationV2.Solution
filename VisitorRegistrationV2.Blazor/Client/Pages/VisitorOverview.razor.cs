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
    public class VisitorOverviewModel : VisitorOverviewBaseModel
    {
        protected Visitor selectedVisitor { get; set; }
        private HubConnection hubConnection;

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                    .WithUrl(NavManager.ToAbsoluteUri("/visitorhub"))
                    .Build();


            hubConnection.On<string>("ReceiveMessage", (message) =>
            {
                if (message == "UpdatedAll")
                {
                    CallLoadData(message);
                    StateHasChanged();
                }
                if (message == "UpdatedSingle")
                {
                    CallLoadData(message);
                    StateHasChanged();
                }             
            });

            await hubConnection.StartAsync();

            await LoadData();
        }

        protected void CallLoadData(string message)
        {
            switch (message)
            {
                case "UpdatedAll":
                    Task.Run(async () =>
                    {
                        await LoadData();
                    });
                    break;
                case "UpdatedSingle":
                    Task.Run(async () =>
                    {
                        await LoadData();
                    });
                    break;
                default:
                    break;
            }
            //if (message == "UpdatedAll")
            //{
            //    Task.Run(async () =>
            //    {
            //        await LoadData();
            //    });
            //}
            //if(message == "UpdatedSingle")
            //{
            //    Task.Run(async () =>
            //    {
            //        await GetUpdatedUser();
            //    });
            //}
        }

        protected async Task GetUpdatedUser()
        {
            try
            {
                var FoundVisitor = visitors.First(v => v.Id == selectedVisitor.Id);
                FoundVisitor = await Http.GetFromJsonAsync<Visitor>($"api/Visitor/{selectedVisitor.Id}");
                StateHasChanged();
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
                visitors = await Http.GetFromJsonAsync<Visitor[]>("api/Visitor");
                StateHasChanged();
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
            selectedVisitor = null;
            showDialogArrived = false;
            if (visitorThatArrived.ArrivalTime == null || overRide == true)
            {
                visitorThatArrived.ArrivalTime = DateTime.Now;

               using var response = await Http.PutAsJsonAsync($"api/visitor/{visitorThatArrived.Id}", visitorThatArrived);
               {
                    showDialogArrived = false;

                    if (IsConnected)
                    {
                        Message = ResponseManager.GetMessage(response);
                        await SendUpdate();
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
                selectedVisitor = visitorThatArrived;
                showDialogArrived = true;
            }
        }
        protected async Task VisitorDeparted(Visitor visitorThatDeparted, bool overRide)
        {
            selectedVisitor = null;
            showDialogDeparted = false;
            if (visitorThatDeparted.DepartureTime == null || overRide == true)
            {
                visitorThatDeparted.DepartureTime = DateTime.Now;
                using var response = await Http.PutAsJsonAsync($"api/visitor/{visitorThatDeparted.Id}", visitorThatDeparted);
                {
                    showDialogDeparted = false;

                    if (IsConnected)
                    {
                        Message = ResponseManager.GetMessage(response);
                        await SendUpdate();
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
                selectedVisitor = visitorThatDeparted;
                showDialogDeparted = true;
            }
        }

        protected void RedirectToCreatePage()
        {
            NavManager.NavigateTo("/Create");
        }

        Task SendMessage() => hubConnection.SendAsync("SendUpdateNotification", "UpdateAll");
        Task SendUpdate() => hubConnection.SendAsync("SendUpdateNotification", "UpdatedSingle");

        protected bool IsConnected =>
        hubConnection.State == HubConnectionState.Connected;

        protected void Dispose()
        {
            _ = hubConnection.DisposeAsync();
        }
    }
}

