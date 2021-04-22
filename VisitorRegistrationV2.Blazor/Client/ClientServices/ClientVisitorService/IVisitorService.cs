using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistration.Blazor.Client.Models;
using VisitorRegistrationV2.Blazor.Shared;
using VisitorRegistrationV2.Blazor.Shared.DTOs;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices
{
    public interface IVisitorService
    {
        public string Message { get; protected set; }
        public void MessageDisposal();
        public Task<ClientVisitor> VisitorArrives(ClientVisitor visitorThatArrived, bool OverRide, DateTime? time);
        public Task<ClientVisitor> VisitorDeparts(ClientVisitor visitorThatDeparted, bool OverRide, DateTime? time);
    }
}
