using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;
using VisitorRegistrationV2.Blazor.Shared.DTOs;
using VisitorRegistrationV2.Blazor.Shared.TimeObjects.Actual;
using VisitorRegistrationV2.Blazor.Shared.TimeObjects.Expected;

namespace VisitorRegistrationV2.Data.Services.Visitors
{
    public class Visitors : IVisitors
    {
        private readonly ApplicationDbContext context;

        public Visitors(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<ActualTime> AddActualTime(ActualTime actualTime)
        {
            var addedTime = actualTime;

            context.ActualTimes.Add(addedTime);

            context.SaveChanges();

            var result = await GetActualTimeById(addedTime.Id);

            return result;
        }

        public async Task<ExpectedTime> AddExpectedTime(ExpectedTime expectedTime)
        {
            var addedTime = expectedTime;

            context.ExpectedTimes.Add(addedTime);

            context.SaveChanges();

            var result = await GetExpectedTimeById(addedTime.Id);

            return result;
        }

        public async Task<Visitor> AddVisitor(Visitor newVisitor)
        {
            var actualResult = await AddActualTime(newVisitor.TodaysTimes);

            var expectedResult = await AddExpectedTime(newVisitor.ExpectedTimes);

            context.Add(newVisitor);
            context.SaveChanges();

            newVisitor.ActualTimesList.Add(actualResult);
            newVisitor.ExpectedTimesList.Add(expectedResult);

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

        public async Task<ActualTime> GetActualTimeById(int Id)
        {
            var result = context.ActualTimes.Find(Id);

            return await Task.FromResult(result);
        }

        public async Task<ExpectedTime> GetExpectedTimeById(int Id)
        {
            var result = context.ExpectedTimes.Find(Id);

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Visitor>> GetListOfVisitors()
        {
            var result = context.Visitors;

            return await Task.FromResult(result);
        }

        public Task<Visitor> GetVisitorById(int id)
        {
            return Task.FromResult(context.Visitors.Find(id));
        }

        public async Task<List<Visitor>> SearchVisitorsByName(string SearchTerm)
        {
            var result = (context.Visitors
                         .Where(v => v.MiddleName == null ? ($"{v.FirstName} {v.LastName}").Contains(SearchTerm) 
                         : ($"{v.FirstName} {v.MiddleName} {v.LastName}").Contains(SearchTerm))).ToList();

            return await Task.FromResult(result);
        }

        public async Task UpdateVisitor(Visitor updatedVisitor)
        {
            context.Update(updatedVisitor);
            await context.SaveChangesAsync();
        }
    }
}
