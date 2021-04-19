using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Blazor.Client.Components.PresentVisitorsUIComponent
{
    public partial class FilterdPresentVisitorsComponent
    {
        [Parameter]
        public IEnumerable<Visitor> PresentVisitors { get; set; }
        [Parameter]
        public bool showDialogArrived { get; set; }
        [Parameter]
        public bool showDialogDeparted { get; set; }
        [Parameter]
        public Action<Visitor, bool> VisitorArrived { get; set; }
        [Parameter]
        public Action<Visitor, bool> VisitorDeparted { get; set; }

        public void OnVisitorArrived(Visitor visitor, bool overRide)
        {
            VisitorArrived.Invoke(visitor, overRide);
        }
        public void OnVisitorDeparted(Visitor visitor, bool overRide)
        {
            VisitorDeparted.Invoke(visitor, overRide);
        }
    }
}
