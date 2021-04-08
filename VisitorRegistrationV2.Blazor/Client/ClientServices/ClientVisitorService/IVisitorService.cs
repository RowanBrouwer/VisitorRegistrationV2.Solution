using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices
{
    public interface IVisitorService
    {
        public string Message { get; protected set; }
        public void MessageDisposal();
        public Task<Visitor> VisitorArrives(Visitor visitorThatArrived, bool OverRide, DateTime? time);
        public Task<Visitor> VisitorDeparts(Visitor visitorThatDeparted, bool OverRide, DateTime? time);
    }
}
