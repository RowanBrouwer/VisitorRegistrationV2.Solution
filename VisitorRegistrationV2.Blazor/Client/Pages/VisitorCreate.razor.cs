using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Client.PageModels;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Blazor.Client.Pages
{
    public class VisitorCreateModel : VisitorCreateBaseModel
    {
        protected Visitor newVisitor = new Visitor();

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                    .WithUrl(NavManager.ToAbsoluteUri("/visitorhub"))
                    .Build();

            await hubConnection.StartAsync();
        }

        protected async Task saveNewVisitor(Visitor visitor)
        {
            Visitor AddedVisitor;
            using var response = await Http.PostAsJsonAsync("api/Visitor", visitor);
            {
                Message = ResponseManager.GetMessage(response);

                AddedVisitor = await response.Content.ReadFromJsonAsync<Visitor>();
                if (IsConnected)
                {
                    await SendUpdate(AddedVisitor.Id);
                }
            }
            NavigateToDetailPage(AddedVisitor.Id);
        }

        Task SendUpdate(int visitorId) => hubConnection.SendAsync("SendAddNotification", visitorId);

        protected void NavigateToDetailPage(int id)
        {
            NavManager.NavigateTo($"/Details/{id}");
        }

        protected void CancelRedirect()
        {
            NavManager.NavigateTo("/");
        }
        protected void Dispose()
        {
            _ = hubConnection.DisposeAsync();
        }
    }
}
