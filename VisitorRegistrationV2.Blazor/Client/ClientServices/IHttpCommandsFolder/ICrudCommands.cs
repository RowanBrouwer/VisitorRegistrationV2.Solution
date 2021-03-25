using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices.IHttpCommandsFolder
{
    public interface ICrudCommands
    {
        public Task<string> saveNewVisitor(Visitor visitor);
        public Task<List<Visitor>> LoadVisitorOverViewItems();
        public Task<Visitor> GetUpdatedUser(int visitorId);
        public Task<string> VisitorArrived(Visitor visitorThatArrived);
        public Task<string> VisitorDeparted(Visitor visitorThatDeparted);
    }
}
