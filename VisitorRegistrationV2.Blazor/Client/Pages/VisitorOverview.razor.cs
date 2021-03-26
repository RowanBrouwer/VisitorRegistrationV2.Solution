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
        protected Visitor SelectedVisitor { get; set; }

        protected override async Task OnInitializedAsync()
        {
            hubConnection.On("ReceiveMessage", () =>
            {
                CallGetUpdatedUser();
                StateHasChanged();
            });

            hubConnection.On<int>("ReceiveAddedUser", (VisitorId) =>
            {
                 CallGetAddedUserAndAddToList(VisitorId);
                 StateHasChanged();
            });

            await LoadData();
        }

        protected void CallGetAddedUserAndAddToList(int VisitorId)
        {
            Task.Run(async () =>
            {
                await GetAddedUserAndAddToList(VisitorId);
            });
        }

        protected async Task GetAddedUserAndAddToList(int VisitorId)
        {
            var newvisitor = await Http.GetFromJsonAsync<Visitor>($"api/Visitor/{VisitorId}");
            visitors.Add(newvisitor);
            StateHasChanged();
        }

        protected void CallGetUpdatedUser()
        {
            Task.Run(async () =>
            {
                await LoadData();
            });
        }

        protected async Task GetUpdatedUser()
        {
            try
            {
                var FoundVisitor = visitors.First(v => v.Id == SelectedVisitor.Id);
                FoundVisitor = await Http.GetFromJsonAsync<Visitor>($"api/Visitor/{SelectedVisitor.Id}");
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
                visitors = await Http.GetFromJsonAsync<List<Visitor>>("api/Visitor");
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
            SelectedVisitor = null;
            showDialogArrived = false;
            if (visitorThatArrived.ArrivalTime == null || overRide == true)
            {
                visitorThatArrived.ArrivalTime = DateTime.Now;
                visitorThatArrived.DepartureTime = null;

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
                SelectedVisitor = visitorThatDeparted;
                showDialogDeparted = true;
            }
        }

        protected void RedirectToCreatePage()
        {
            NavManager.NavigateTo("/Create");
        }

        Task SendUpdate() => hubConnection.SendAsync("SendUpdateNotification");

        protected void Dispose()
        {
            _ = hubConnection.DisposeAsync();
        }
    }
}

