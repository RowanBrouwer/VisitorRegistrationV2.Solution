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
        protected Visitor newVisitor;

        protected override async Task OnInitializedAsync()
        {
            await Task.FromResult(newVisitor = new Visitor());
        }

        protected async Task saveNewVisitor(Visitor visitor)
        {
            Visitor AddedVisitor;
            if (SignalRService.IsConnected)
            {
                using var response = await Http.PostAsJsonAsync("api/Visitor", visitor);
                {
                    Message = ResponseManager.GetMessage(response);

                    AddedVisitor = await response.Content.ReadFromJsonAsync<Visitor>();

                    await SignalRService.SendAddNotification(AddedVisitor.Id);

                }
                NavigateToDetailPage(AddedVisitor.Id);
            }
            else
            {
                Message = "No Connection";
            }
        }

        protected void NavigateToDetailPage(int id)
        {
            NavManager.NavigateTo($"/Details/{id}");
        }

        protected void CancelRedirect()
        {
            NavManager.NavigateTo("/");
        }
    }
}
