using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;
using VisitorRegistrationV2.Blazor.Shared.DTOs;

namespace VisitorRegistrationV2.Blazor.Client.Components.DepartedUIButton
{
    public partial class VisitorDepartedComponent
    {
        [Parameter]
        public VisitorDTO SelectedVisitor { get; set; }
        [Parameter]
        public Action<VisitorDTO, bool> VisitorDeparted { get; set; }
        [Parameter]
        public bool showDialogDeparted { get; set; }
    }
}
