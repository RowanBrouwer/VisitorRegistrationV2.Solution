﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Blazor.Client.PageModels
{
    public class VisitorOverviewBaseModel : PageBaseModel
    {
        [Parameter]
        public string SearchTerm { get; set; } = "";
        protected bool showDialogArrived { get; set; } = false;
        protected bool showDialogDeparted { get; set; } = false;
        protected IEnumerable<Visitor> visitors { get; set; } = new List<Visitor>();
        protected IEnumerable<Visitor> filterdVisitors =>  visitors.Where(a => a.FullName().ToLower().Contains(SearchTerm.ToLower()));
        protected bool IsEmpty { get; set; } = false;
    }
}
