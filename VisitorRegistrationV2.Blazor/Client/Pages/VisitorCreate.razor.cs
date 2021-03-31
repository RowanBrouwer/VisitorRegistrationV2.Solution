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
        protected Visitor newVisitor = new();

        //protected override async Task OnInitializedAsync()
        //{
        //}

        protected async Task saveNewVisitor(Visitor visitor)
        {
            Visitor AddedVisitor;
            if (SignalRService.IsConnected)
            {
                var response = await Http.AddVisitor(visitor);

                Message = ResponseManager.GetMessage(response);

                AddedVisitor = await response.Content.ReadFromJsonAsync<Visitor>();

                await SignalRService.SendAddNotification(AddedVisitor.Id);
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
