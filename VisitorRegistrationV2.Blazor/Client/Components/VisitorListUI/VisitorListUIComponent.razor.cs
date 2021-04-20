using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;
using VisitorRegistrationV2.Blazor.Shared.DTOs;

namespace VisitorRegistrationV2.Blazor.Client.Components.VisitorListUI
{
    public partial class VisitorListUIComponent
    {
        [Parameter]
        public IEnumerable<VisitorDTO> Visitors { get; set; }
        [Parameter]
        public bool showDialogArrived { get; set; }
        [Parameter]
        public bool showDialogDeparted { get; set; }
        [Parameter]
        public Action<VisitorDTO, bool> VisitorArrived { get; set; }
        [Parameter]
        public Action<VisitorDTO, bool> VisitorDeparted { get; set; }
        [Parameter]
        public bool RenderDepartureTime { get; set; }
        public void OnVisitorArrived(VisitorDTO visitor, bool overRide)
        {
            VisitorArrived.Invoke(visitor, overRide);
        }
        public void OnVisitorDeparted(VisitorDTO visitor, bool overRide)
        {
            VisitorDeparted.Invoke(visitor, overRide);
        }
    }
}
