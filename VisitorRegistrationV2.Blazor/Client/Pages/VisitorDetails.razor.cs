using Microsoft.AspNetCore.Components;
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
    public class VisitorDetailsModel : PageBaseModel
    {
        [Parameter]
        public int VisitorId { get; set; }
        protected Visitor visitor { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                visitor = await Http.GetVisitor(VisitorId);
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }

        protected void RedirectToOverview()
        {
            NavManager.NavigateTo("/");
        }
    }
}
