using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace VisitorRegistrationV2.Blazor.Client.PageModels
{
    public class PageBaseModel : ComponentBase
    {
        [Inject]
        protected HttpClient Http { get; set; }
        [Inject]
        protected NavigationManager NavManager { get; set; }
    }
}
