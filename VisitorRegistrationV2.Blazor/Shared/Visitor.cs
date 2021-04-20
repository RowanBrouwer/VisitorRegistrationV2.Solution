using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared.TimeObjects.Actual;
using VisitorRegistrationV2.Blazor.Shared.TimeObjects.Expected;

namespace VisitorRegistrationV2.Blazor.Shared
{
    public class Visitor
    {
        [Key]
        public int Id { get; set; }     
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public List<ActualTime> ActualTimes { get; set; }
        public List<ExpectedTime> ExpectedTimes { get; set; }

        public DateTime? ExpectedArrivalTime() => ExpectedTimes.Where(a => a.ArrivalTime.Value.Date == DateTime.Now.Date).First().ArrivalTime;
        public DateTime? ExptectedDepartureTime() => ActualTimes.Where(a => a.ArrivalTime.Value.Date == DateTime.Now.Date).First().DepartureTime;
        public DateTime? TodaysArrivalTime() => ActualTimes.Where(a => a.ArrivalTime.Value.Date == DateTime.Now.Date).First().ArrivalTime;
        public DateTime? TodaysDepartureTime() => ActualTimes.Where(a => a.ArrivalTime.Value.Date == DateTime.Now.Date).First().DepartureTime;
        public string FullName => string.IsNullOrEmpty(MiddleName) ? $"{FirstName} {LastName}" : $"{FirstName} {MiddleName} {LastName}";

        public Task<string> GetDateTimeAsString(DateTime? dateTime)
        {
            if (dateTime != null)
            {
                if (dateTime.Value.Date == DateTime.Today)
                {
                    return Task.FromResult(dateTime.Value.ToShortTimeString());
                }
                else
                {
                    return Task.FromResult(dateTime.Value.ToShortDateString() + ' ' + dateTime.Value.ToShortTimeString());
                }
            }
            else
            {
                return Task.FromResult("");
            }
        }
    }
}
