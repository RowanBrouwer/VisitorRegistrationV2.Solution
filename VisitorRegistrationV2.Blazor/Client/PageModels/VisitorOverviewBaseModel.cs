using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using VisitorRegistrationV2.Blazor.Shared;
using VisitorRegistrationV2.Blazor.Shared.DTOs;

namespace VisitorRegistrationV2.Blazor.Client.PageModels
{
    public class VisitorOverviewBaseModel : PageBaseModel
    {
        [Parameter]
        public string SearchTerm { get; set; } = "";
        public bool showDialogArrived { get; set; }
        public bool showDialogDeparted { get; set; }

        /// <summary>
        /// List of all visitors.
        /// </summary>
        protected List<VisitorDTO> visitors { get; set; } = new List<VisitorDTO>();

        /// <summary>
        /// List of all currently present visitors filterd by their fullname.
        /// </summary>
        protected IEnumerable<VisitorDTO> FilterdPresentVisitors => visitors
            .OrderBy(v => v.TodaysArrivalTime)
            .Where(a => a.TodaysArrivalTime != null
            && a.TodaysDepartureTime == null && a.FullName
            .ToLower().Contains(SearchTerm.ToLower()));

        /// <summary>
        /// List of all currently not present visitors filterd by their fullname.
        /// </summary>
        protected IEnumerable<VisitorDTO> FilterdNotPresentVisitors => visitors
                .OrderBy(v => v.TodaysArrivalTime)
                .Where(a => (a.TodaysDepartureTime == null ? a.TodaysDepartureTime == null 
                && a.TodaysArrivalTime == null : a.TodaysDepartureTime != null 
                && a.TodaysArrivalTime != null) && a.FullName
                .ToLower().Contains(SearchTerm.ToLower()));
    }
}
