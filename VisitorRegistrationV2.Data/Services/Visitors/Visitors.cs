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

            var result = await GetVisitorById(newVisitor.Id);

            return result;
        }

        public Task DeleteVisitor(int id)
        {
            var VisitorToDelete = context.Visitors.Find(id);
            context.Visitors.Remove(VisitorToDelete);
            context.SaveChanges();

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

        public async Task<IEnumerable<Visitor>> SearchVisitorsByName(string SearchTerm)
        {
            var result = (context.Visitors
                         .Where(v => v.MiddleName == null ? ($"{v.FirstName} {v.LastName}").Contains(SearchTerm) 
                         : ($"{v.FirstName} {v.MiddleName} {v.LastName}").Contains(SearchTerm)));

            return await Task.FromResult(result);
        }

        public async Task UpdateVisitor(Visitor updatedVisitor)
        {
            context.Update(updatedVisitor);
            await context.SaveChangesAsync();
        }
    }
}
