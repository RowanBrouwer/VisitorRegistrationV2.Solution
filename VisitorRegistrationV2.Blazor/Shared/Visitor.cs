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
                Id = visitorDTO.Id;
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

                DateTime? todayArrival = null;
                DateTime? todayDeparture = null;

                if (visitorDTO.TodaysArrivalTime != null)
                {
                    todayArrival = visitorDTO.TodaysArrivalTime.StringToNullableDateTime();
                }

                if (visitorDTO.TodaysDepartureTime != null)
                {
                    todayDeparture = visitorDTO.TodaysDepartureTime.StringToNullableDateTime();
                }

                TodaysTimes = new ActualTime() {  ArrivalTime = todayArrival, DepartureTime = todayDeparture };
            }
            else
            {
                TodaysTimes = ActualTimesList.Find(t => t.Id == visitorDTO.Id);

                if (visitorDTO.TodaysArrivalTime != null)
                {
                    TodaysTimes.ArrivalTime = visitorDTO.TodaysArrivalTime.StringToNullableDateTime();
                }

                if (visitorDTO.TodaysDepartureTime != null)
                {
                    TodaysTimes.DepartureTime = visitorDTO.TodaysDepartureTime.StringToNullableDateTime();
                }
            }

            if (ExpectedTimesList.Find(t => t.Id == visitorDTO.ExpectedTimeObjId) == null)
            {

                DateTime? expectedArrival = null;
                DateTime? expectedDeparture = null;

                if (visitorDTO.ExpectedArrivalTime != null)
                {
                    expectedArrival = visitorDTO.TodaysArrivalTime.StringToNullableDateTime();
                }

                if (visitorDTO.ExpectedDepartureTime != null)
                {
                    expectedDeparture = visitorDTO.TodaysDepartureTime.StringToNullableDateTime();
                }

                ExpectedTimes = new ExpectedTime() { ArrivalTime = expectedArrival, DepartureTime = expectedDeparture };
            }
            else
            {
                ExpectedTimes = ExpectedTimesList.Find(t => t.Id == visitorDTO.ExpectedTimeObjId);

                if (visitorDTO.TodaysArrivalTime != null)
                {
                    ExpectedTimes.ArrivalTime = visitorDTO.TodaysArrivalTime.StringToNullableDateTime();
                }

                if (visitorDTO.TodaysDepartureTime != null)
                {
                    ExpectedTimes.DepartureTime = visitorDTO.TodaysDepartureTime.StringToNullableDateTime();
                }
            }
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
            VisitorDTO visitorDTO = new();

            visitorDTO.Id = this.Id;
            visitorDTO.FirstName = this.FirstName;
            visitorDTO.MiddleName = this.MiddleName;
            visitorDTO.LastName = this.LastName;
            visitorDTO.ExpectedTimeObjId = this.ExpectedTimes == null ? 0 : this.ExpectedTimes.Id;
            if (ExpectedTimes != null)
            {
                visitorDTO.ExpectedArrivalTime = ExpectedTimes.ArrivalTime != null ? this.ExpectedTimes.ArrivalTime.ToString() : null;
                visitorDTO.ExpectedDepartureTime = ExpectedTimes.DepartureTime != null ? this.ExpectedTimes.ArrivalTime.ToString() : null;
            }

            visitorDTO.TodaysTimeObjId = this.TodaysTimes == null ? 0 : this.TodaysTimes.Id;
            if (TodaysTimes != null)
            {
                visitorDTO.TodaysArrivalTime = TodaysTimes.ArrivalTime != null ? this.TodaysTimes.ArrivalTime.ToString() : null;
                visitorDTO.TodaysDepartureTime = TodaysTimes.DepartureTime != null ? this.TodaysTimes.ArrivalTime.ToString() : null;
            }

            return Task.FromResult(visitorDTO);
        }

        public Task ConvertFromDTO(VisitorDTO visitorDTO)
        {
            TodaysTimes.Id = visitorDTO.Id;

            FirstName = visitorDTO.FirstName;
            MiddleName = visitorDTO.MiddleName;
            LastName = visitorDTO.LastName;

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

            return Task.CompletedTask;
        }
    }
}
