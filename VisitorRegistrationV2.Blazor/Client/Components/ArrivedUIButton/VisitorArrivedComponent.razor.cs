using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistration.Blazor.Client.Models;
using VisitorRegistrationV2.Blazor.Shared.DTOs;

namespace VisitorRegistrationV2.Blazor.Client.Components.ArrivedUIButton
{
    public partial class VisitorArrivedComponent
    {
        [Parameter]
        public ClientVisitor SelectedVisitor { get; set; }
        [Parameter]
        public Action<ClientVisitor, bool> VisitorArrived { get; set; }
        [Parameter]
        public bool showDialogArrived { get; set; }
    }
}
