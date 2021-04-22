using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared.DTOs;
using VisitorRegistrationV2.Blazor.Shared.TimeObjects.Actual;
using VisitorRegistrationV2.Blazor.Shared.TimeObjects.Expected;
using VisitorRegistrationV2.Blazor.Shared.ExtensionMethods;

namespace VisitorRegistrationV2.Blazor.Shared
{
    public class Visitor
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public Visitor()
        {

            ActualTimesList = new List<ActualTime>();
            ExpectedTimesList = new List<ExpectedTime>();

            ExpectedTimes = ExpectedTimesList.Where(a => a.ArrivalTime.Value.Date == DateTime.Now.Date).First();
            TodaysTimes = ActualTimesList.Where(a => a.ArrivalTime.Value.Date == DateTime.Now.Date).First();

        }

        public ExpectedTime ExpectedTimes { get; set; }
        public ActualTime TodaysTimes { get; set; }

        public List<ActualTime> ActualTimesList { get; set; }
        public List<ExpectedTime> ExpectedTimesList { get; set; }

        public string FullName => string.IsNullOrEmpty(MiddleName) ? $"{FirstName} {LastName}" : $"{FirstName} {MiddleName} {LastName}";

        public Visitor(VisitorDTO visitorDTO)
        {
            if (visitorDTO.Id != 0)
            {
                TodaysTimes.Id = visitorDTO.Id;
            }

            FirstName = visitorDTO.FirstName;
            MiddleName = visitorDTO.MiddleName;
            LastName = visitorDTO.LastName;

            if (ExpectedTimesList == null)
            {
                ExpectedTimesList = new List<ExpectedTime>();
            }

            if (ActualTimesList == null)
            {
                ActualTimesList = new List<ActualTime>();
            }

            if (ActualTimesList.Find(t => t.Id == visitorDTO.TodaysTimeObjId) == null)
            {
                TodaysTimes = new();
            }
            else
            {
                TodaysTimes = ActualTimesList.Find(t => t.Id == visitorDTO.Id);
            }

            TodaysTimes.ArrivalTime = visitorDTO.TodaysArrivalTime.StringToNullableDateTime();
            TodaysTimes.DepartureTime = visitorDTO.TodaysDepartureTime.StringToNullableDateTime();

            ActualTimesList.Add(TodaysTimes);

            if (ExpectedTimesList.Find(t => t.Id == visitorDTO.ExpectedTimeObjId) == null)
            {
                ExpectedTimes = new();
            }
            else
            {
                ExpectedTimes = ExpectedTimesList.Find(t => t.Id == visitorDTO.ExpectedTimeObjId);
            }

            ExpectedTimes.ArrivalTime = visitorDTO.ExpectedArrivalTime.StringToNullableDateTime();
            ExpectedTimes.DepartureTime = visitorDTO.ExpectedDepartureTime.StringToNullableDateTime();

            ExpectedTimesList.Add(ExpectedTimes);
        }


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

        public Task<VisitorDTO> ConvertToDTO()
        {
            VisitorDTO visitorDTO = new()
            {
                Id = this.Id,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                LastName = this.LastName,
                ExpectedTimeObjId = this.ExpectedTimes.Id,
                ExpectedArrivalTime = this.ExpectedTimes.ArrivalTime.ToString(),
                ExpectedDepartureTime = this.ExpectedTimes.DepartureTime.ToString(),
                TodaysTimeObjId = this.TodaysTimes.Id,
                TodaysArrivalTime = this.TodaysTimes.ArrivalTime.ToString(),
                TodaysDepartureTime = this.TodaysTimes.DepartureTime.ToString()   
            };

            return Task.FromResult(visitorDTO);
        }
    }
}
