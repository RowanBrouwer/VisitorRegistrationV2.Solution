using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared.TimeObjects.Base;

namespace VisitorRegistrationV2.Blazor.Shared.TimeObjects.Expected
{
    public class ExpectedTime : TimeObj
    {
        public int Id { get; set; }

        public Task<bool> SetArrivalTime(DateTime? time, bool overRide)
            => ArrivalTime == null ?
                Task.FromResult((true, ArrivalTime = time).Item1)
                : (overRide == true ?
                Task.FromResult((true, ArrivalTime = time, DepartureTime = null).Item1)
                : Task.FromResult(false));

        public Task<bool> SetDepartureTime(DateTime? time, bool overRide)
            => DepartureTime == null && ArrivalTime.HasValue ?
                Task.FromResult((true, DepartureTime = time).Item1)
                : (overRide == true && ArrivalTime.HasValue ?
                Task.FromResult((true, DepartureTime = time).Item1)
                : Task.FromResult(false));
    }
}
