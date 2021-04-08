using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Blazor.Client.PageModels
{
    public class VisitorOverviewBaseModel : PageBaseModel
    {
        [Parameter]
        public string SearchTerm { get; set; } = "";
        protected bool showDialogArrived { get; set; } = false;
        protected bool showDialogDeparted { get; set; } = false;

        /// <summary>
        /// List of all visitors.
        /// </summary>
        protected List<Visitor> visitors { get; set; } = new List<Visitor>();

        /// <summary>
        /// List of all currently present visitors filterd by their fullname.
        /// </summary>
        protected IEnumerable<Visitor> FilterdPresentVisitors => visitors
            .OrderBy(v => v.ArrivalTime)
            .Where(a => a.ArrivalTime != null
            && a.DepartureTime == null && a.FullName()
            .ToLower().Contains(SearchTerm.ToLower()));

        /// <summary>
        /// List of all currently not present visitors filterd by their fullname.
        /// </summary>
        protected IEnumerable<Visitor> FilterdNotPresentVisitors => visitors
                .OrderBy(v => v.ArrivalTime)
                .Where(a => (a.DepartureTime == null ? a.DepartureTime == null 
                && a.ArrivalTime == null : a.DepartureTime != null 
                && a.ArrivalTime != null) && a.FullName()
                .ToLower().Contains(SearchTerm.ToLower()));
    }
}
