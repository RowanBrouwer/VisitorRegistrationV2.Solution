using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistration.Blazor.Client.Models;
using VisitorRegistrationV2.Blazor.Shared;
using VisitorRegistrationV2.Blazor.Shared.DTOs;

namespace VisitorRegistrationV2.Blazor.Client.Components.VisitorListUI
{
    public partial class VisitorListUIComponent
    {
        [Parameter]
        public IEnumerable<ClientVisitor> Visitors { get; set; }
        [Parameter]
        public bool showDialogArrived { get; set; }
        [Parameter]
        public bool showDialogDeparted { get; set; }
        [Parameter]
        public Action<ClientVisitor, bool> VisitorArrived { get; set; }
        [Parameter]
        public Action<ClientVisitor, bool> VisitorDeparted { get; set; }
        [Parameter]
        public bool RenderDepartureTime { get; set; }
        public void OnVisitorArrived(ClientVisitor visitor, bool overRide)
        {
            VisitorArrived.Invoke(visitor, overRide);
        }
        public void OnVisitorDeparted(ClientVisitor visitor, bool overRide)
        {
            VisitorDeparted.Invoke(visitor, overRide);
        }
    }
}
