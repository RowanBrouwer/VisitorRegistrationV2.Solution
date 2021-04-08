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
        private string firstName;
        private string middleName;
        private string lastName;
        private DateTime? arrivalTime;
        private DateTime? departureTime;

        public string FirstName { get => firstName; set => firstName = value; }
        public string MiddleName { get => middleName; set => middleName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime? ArrivalTime { get => arrivalTime; set => arrivalTime = value; }
        public DateTime? DepartureTime { get => departureTime; set => departureTime = value; }

        public string FullName() => (middleName == null) ? $"{firstName} {lastName}" : $"{firstName} {middleName} {lastName}";

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
