using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Data.Services.Visitors
{
    public class Visitors : IVisitors
    {
        private readonly ApplicationDbContext context;
                 
        public Visitors(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Visitor> AddVisitor(Visitor newVisitor)
        {
            context.Add(newVisitor);
            context.SaveChanges();
            return await Task.FromResult(context.Visitors.Find(newVisitor));
        }

        public Task DeleteVisitor(int id)
        {
            var VisitorToDelete = context.Visitors.Find(id);
            context.Visitors.Remove(VisitorToDelete);

            return Task.FromResult(true);
        }

        public async Task<IEnumerable<Visitor>> GetListOfVisitors()
        {
            var result = context.Visitors.OrderBy(a => a.ArrivalTime);

            return await Task.FromResult(result);
        }

        public Task<Visitor> GetVisitorById(int id)
        {
            return Task.FromResult(context.Visitors.Find(id));
        }

        public async Task<Visitor> GetVisitorByName(string Name)
        {
            return await Task.FromResult(
                context.Visitors
                .Where(a => a.FullName()
                .ToLower()
                .Contains(Name.ToLower()))
                .FirstOrDefault());
        }

        public async Task<IEnumerable<Visitor>> GetVisitorListBySearchTerm(string searchTerm)
        {
            return await Task.FromResult(
                context.Visitors
                .Where(a => a.FullName()
                .ToLower()
                .Contains(searchTerm.ToLower())));
        }

        public async Task UpdateVisitor(Visitor updatedVisitor)
        {
            var UserToUpdate = await GetVisitorById(updatedVisitor.Id);

            if (UserToUpdate.FirstName != updatedVisitor.FirstName)
                UserToUpdate.FirstName = updatedVisitor.FirstName;

            if (UserToUpdate.MiddleName != updatedVisitor.MiddleName)
                UserToUpdate.MiddleName = updatedVisitor.MiddleName;

            if (UserToUpdate.LastName != updatedVisitor.LastName)
                UserToUpdate.LastName = updatedVisitor.LastName;

            if (UserToUpdate.ArrivalTime != updatedVisitor.ArrivalTime)
                UserToUpdate.ArrivalTime = updatedVisitor.ArrivalTime;

            if (UserToUpdate.DepartureTime != updatedVisitor.DepartureTime)
                UserToUpdate.DepartureTime = updatedVisitor.DepartureTime;

            context.Update(UserToUpdate);
            context.SaveChanges();
        }
    }
}
