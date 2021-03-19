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
        protected Visitor newVisitor { get; set; }
        protected override Task OnInitializedAsync()
        {
            return Task.FromResult(newVisitor = new Visitor());
        }

        protected async Task saveNewVisitor(Visitor visitor)
        {
            Visitor AddedVisitor;
            using var response = await Http.PostAsJsonAsync("api/Visitor", visitor);
            {
                Message = ResponseManager.GetMessage(response);

                AddedVisitor = await response.Content.ReadFromJsonAsync<Visitor>();
            }

            NavigateToDetailPage(AddedVisitor.Id);
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
