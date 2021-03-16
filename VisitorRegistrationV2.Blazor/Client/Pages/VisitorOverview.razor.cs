using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Client.PageModels;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Blazor.Client.Pages
{
    public class VisitorOverviewModel : VisitorOverviewBaseModel
    {
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

        protected async Task CheckForFilterd()
        {
            var result = await Task.FromResult( Http.GetFromJsonAsync<Visitor[]>("api/Visitor/FilterCheck"));

            if (result == null)
            {
                
            }
        }

        protected async Task VisitorArrived(Visitor visitorThatArrived, bool overRide)
        {
            if (visitorThatArrived.ArrivalTime == null || overRide == true)
            {
                visitorThatArrived.ArrivalTime = DateTime.Now;
                await Http.PutAsJsonAsync($"api/visitor{visitorThatArrived.Id}", visitorThatArrived);
                showDialogArrived = false;
            }
            else
            {
                showDialogArrived = true;
            }
        }
        protected async Task VisitorDeparted(Visitor visitorThatDeparted, bool overRide)
        {
            if (visitorThatDeparted.DepartureTime == null || overRide == true)
            {
                visitorThatDeparted.DepartureTime = DateTime.Now;
                await Http.PutAsJsonAsync($"api/visitor{visitorThatDeparted.Id}", visitorThatDeparted);
                showDialogDeparted = false;
            }
            else
            {
                showDialogDeparted = true;
            }
        }
    }
}
