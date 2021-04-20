using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Client.PageModels;
using VisitorRegistrationV2.Blazor.Shared;
using VisitorRegistrationV2.Blazor.Shared.DTOs;

namespace VisitorRegistrationV2.Blazor.Client.Pages
{
    public class VisitorCreateModel : VisitorCreateBaseModel
    {
        protected VisitorDTO newVisitor = new();

        /// <summary>
        /// Requests an save action.
        /// </summary>
        /// <param name="visitor"> visitor that needs to be saved </param>
        /// <returns></returns>
        protected async Task saveNewVisitor(VisitorDTO visitor)
        {
            VisitorDTO AddedVisitor;
            if (await SignalRService.IsConnected())
            {
                var response = await Http.AddVisitor(visitor);

                Message = ResponseManager.GetMessage(response);
                await delayMessageReset();

                AddedVisitor = await response.Content.ReadFromJsonAsync<VisitorDTO>();

                await SignalRService.SendAddedVisitorNotification(AddedVisitor.Id);
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
