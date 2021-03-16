using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Data.Services.Visitors
{
    public interface IVisitors
    {
        public Task<IEnumerable<Visitor>> GetListOfVisitors();
        public Task<Visitor> AddVisitor(Visitor newVisitor);
        public Task DeleteVisitor(int id);
        public Task UpdateVisitor(Visitor updatedVisitor);
        public Task<Visitor> GetVisitorById(int id);
        public Task<Visitor> GetVisitorByName(string firstName, string middleName, string lastName);

    }
}
