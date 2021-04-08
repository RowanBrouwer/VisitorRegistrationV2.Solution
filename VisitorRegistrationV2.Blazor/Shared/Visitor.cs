using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VisitorRegistrationV2.Blazor.Shared
{
    public class Visitor
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public DateTime? DepartureTime { get; set; }
        public string FullName() => (MiddleName == null) ? $"{FirstName} {LastName}" : $"{FirstName} {MiddleName} {LastName}";

        public Task<bool> OverrideArrivalTime(DateTime? time, bool overRide)
            => SetArrivalTimeToNow(time, overRide);

        public Task<bool> OverrideDepartureTime(DateTime? time, bool overRide)
            => SetDepartureTimeToNow(time, overRide);

        private Task<bool> SetArrivalTimeToNow(DateTime? time, bool overRide)
        {
            if (time != null)
            {
                if (ArrivalTime != null)
                {
                    if (overRide == true)
                    {
                        ArrivalTime = time;
                        return Task.FromResult(true);
                    }
                    else
                    {
                        return Task.FromResult(false);
                    }
                }
                else
                {
                    if (overRide == true)
                    {
                        ArrivalTime = time;
                        if (DepartureTime != null)
                        {
                            DepartureTime = null;
                        }
                        return Task.FromResult(true);
                    }
                    else
                    {
                        return Task.FromResult(false);
                    }
                }
            }
            else
            {
                if (ArrivalTime == null && DepartureTime == null)
                {
                    ArrivalTime = DateTime.Now;
                    return Task.FromResult(true);
                }
                else
                {
                    if (overRide == true)
                    {
                        ArrivalTime = DateTime.Now;
                        if (DepartureTime != null)
                        {
                            DepartureTime = null;
                        }
                        return Task.FromResult(true);
                    }
                    else
                    {
                        return Task.FromResult(false);
                    }
                }
            }
        }

        private Task<bool> SetDepartureTimeToNow(DateTime? time, bool overRide)
        {
            if (time != null)
            {
                if (DepartureTime != null)
                {
                    DepartureTime = time;
                    return Task.FromResult(true);
                }
                else
                {
                    if (overRide == true)
                    {
                        DepartureTime = time;
                        return Task.FromResult(true);
                    }
                    else
                    {
                        return Task.FromResult(false);
                    }
                }
            }
            else
            {
                if (DepartureTime == null)
                {
                    DepartureTime = DateTime.Now;
                    return Task.FromResult(true);
                }
                else
                {
                    if (overRide == true)
                    {
                        DepartureTime = DateTime.Now;
                        return Task.FromResult(true);
                    }
                    else
                    {
                        return Task.FromResult(false);
                    }
                }
            }
        }

        public Task<bool> SetArrivalTime(DateTime? time, bool overRide)
        {
            bool timeHasValue = time.HasValue;
            bool arrivalTimeHasValue = ArrivalTime.HasValue;
            bool departureTimeHasValue = DepartureTime.HasValue;

            switch (timeHasValue, overRide, arrivalTimeHasValue, departureTimeHasValue)
            {
                case (true, true, false, false):
                    ArrivalTime = time;
                    return Task.FromResult(true);
                case (true, true, true, false):
                    ArrivalTime = time;
                    return Task.FromResult(true);
                case (false, true, false, false):
                    ArrivalTime = DateTime.Now;
                    return Task.FromResult(true);
                case (false, true, true, false):
                    ArrivalTime = DateTime.Now;
                    return Task.FromResult(true);
                case (true, true, false, true):
                    ArrivalTime = time;
                    DepartureTime = null;
                    return Task.FromResult(true);
                case (true, true, true, true):
                    ArrivalTime = time;
                    DepartureTime = null;
                    return Task.FromResult(true);
                case (false, true, false, true):
                    ArrivalTime = DateTime.Now;
                    DepartureTime = null;
                    return Task.FromResult(true);
                case (false, true, true, true):
                    ArrivalTime = DateTime.Now;
                    DepartureTime = null;
                    return Task.FromResult(true);
                case (false, false, false, false):
                    ArrivalTime = DateTime.Now;
                    return Task.FromResult(true);
                case (false, false, false, true):
                    ArrivalTime = DateTime.Now;
                    DepartureTime = null;
                    return Task.FromResult(true);
                case (true, false, false, false):
                    ArrivalTime = time;
                    return Task.FromResult(true);
                default:
                    return Task.FromResult(false);
            }
        }

        public Task<bool> SetDepartureTime(DateTime? time, bool overRide)
        {
            bool timeHasValue = time.HasValue;
            bool arrivalTimeHasValue = ArrivalTime.HasValue;
            bool departureTimeHasValue = DepartureTime.HasValue;

            switch (timeHasValue, overRide, departureTimeHasValue, arrivalTimeHasValue)
            {
                case (true, true, false, true):
                    DepartureTime = time;
                    return Task.FromResult(true);
                case (true, true, true, true):
                    DepartureTime = time;
                    return Task.FromResult(true);
                case (false, true, false, true):
                    DepartureTime = DateTime.Now;
                    return Task.FromResult(true);
                case (false, true, true, true):
                    DepartureTime = DateTime.Now;
                    return Task.FromResult(true);
                case (false, false, false, true):
                    DepartureTime = DateTime.Now;
                    return Task.FromResult(true);
                default:
                    return Task.FromResult(false);
            }
        }
    }
}
