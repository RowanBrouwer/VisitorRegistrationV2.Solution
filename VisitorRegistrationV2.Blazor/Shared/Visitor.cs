using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
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
        public string FullName() => (MiddleName == null) ? $"{FirstName} {LastName}" : $"{FirstName} {MiddleName} {LastName}";
        public DateTime? ArrivalTime { get; set; }
        public DateTime? DepartureTime { get; set; }
    }
}
