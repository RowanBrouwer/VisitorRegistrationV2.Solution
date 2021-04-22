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

        public async Task<ActualTime> AddActualTime(DateTime? arrival, DateTime? departure)
        {
            var addedTime = new ActualTime { ArrivalTime = arrival, DepartureTime = departure };
            context.Add(addedTime);

            context.SaveChanges();

            var result = await GetActualTimeById(addedTime.Id);

            return result;
        }

        public async Task<ExpectedTime> AddExpectedTime(DateTime? arrival, DateTime? departure)
        {
            var addedTime = new ExpectedTime { ArrivalTime = arrival, DepartureTime = departure };

            context.Add(addedTime);

            context.SaveChanges();

            var result = await GetExpectedTimeById(addedTime.Id);

            return result;
        }

        public async Task<Visitor> AddVisitor(VisitorDTO newVisitor)
        {
            var actualResult = await AddActualTime(newVisitor.TodaysArrivalTime, newVisitor.TodaysDepartureTime);

            var expectedResult = await AddExpectedTime(newVisitor.TodaysArrivalTime, newVisitor.TodaysDepartureTime);

            Visitor ConvertedDto = await VisitorDTOToVisitor(newVisitor);
            ConvertedDto.ActualTimesList = new List<ActualTime>();
            ConvertedDto.ExpectedTimes = new List<ExpectedTime>();

            context.Add(ConvertedDto);
            context.SaveChanges();

            ConvertedDto.ActualTimesList.Add(actualResult);
            ConvertedDto.ExpectedTimes.Add(expectedResult);

            context.SaveChanges();

            var result = await GetVisitorById(ConvertedDto.Id);

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

        public async Task<Visitor> VisitorDTOToVisitor(VisitorDTO visitorDTO)
        {
            var result = await GetVisitorById(visitorDTO.Id);

            if (result == null)
            {
                var visitor = new Visitor()
                {
                    FirstName = visitorDTO.FirstName,
                    MiddleName = visitorDTO.MiddleName,
                    LastName = visitorDTO.LastName,
                };
                return visitor;
            }

            return result;
        }

        public async Task<VisitorDTO> VisitorToVisitorDTO(Visitor visitor)
        {
            var result = new VisitorDTO()
            {
                Id = visitor.Id,
                ExpectedArrivalTime = visitor.ExpectedArrivalTime(),
                ExpectedDepartureTime = visitor.ExptectedDepartureTime(),
                TodaysArrivalTime = visitor.TodaysArrivalTime(),
                TodaysDepartureTime = visitor.TodaysDepartureTime(),
                FirstName = visitor.FirstName,
                MiddleName = visitor.MiddleName,
                LastName = visitor.LastName,
                FullName = visitor.FullName
            };

            return await Task.FromResult(result);
        }
    }
}
