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
        public string FullName => string.IsNullOrEmpty(MiddleName) ? $"{FirstName} {LastName}" : $"{FirstName} {MiddleName} {LastName}";

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
