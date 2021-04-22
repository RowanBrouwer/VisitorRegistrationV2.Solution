using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared.TimeObjects.Actual;
using VisitorRegistrationV2.Blazor.Shared.TimeObjects.Expected;

namespace VisitorRegistrationV2.Blazor.Shared.DTOs
{
    public class VisitorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int ExpectedTimeObjId { get; set; }
        public string ExpectedArrivalTime { get; set; }
        public string ExpectedDepartureTime { get; set; }
        public int TodaysTimeObjId { get; set; }
        public string TodaysArrivalTime { get; set; }
        public string TodaysDepartureTime { get; set; }
    }
}
