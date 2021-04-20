﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;
using VisitorRegistrationV2.Blazor.Shared.DTOs;

namespace VisitorRegistrationV2.Blazor.Client.Components.ArrivedUIButton
{
    public partial class VisitorArrivedComponent
    {
        [Parameter]
        public VisitorDTO SelectedVisitor { get; set; }
        [Parameter]
        public Action<VisitorDTO, bool> VisitorArrived { get; set; }
        [Parameter]
        public bool showDialogArrived { get; set; }
    }
}
