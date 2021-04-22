using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared.DTOs;
using VisitorRegistrationV2.Blazor.Shared.ExtensionMethods;

namespace VisitorRegistration.Blazor.Client.Models
{
    public class ClientVisitor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        private int ExpectedTimeObjId { get; set; }
        public DateTime? ExpectedArrivalTime { get; set; }
        public DateTime? ExpectedDepartureTime { get; set; }
        private int TodaysTimeObjId { get; set; }
        public DateTime? TodaysArrivalTime { get; set; }
        public DateTime? TodaysDepartureTime { get; set; }
        public string FullName { get; set; }


        public ClientVisitor(VisitorDTO visitorDTO)
        {
            ConvertFromDto(visitorDTO);
        }

        public ClientVisitor()
        {

        }

        public Task<bool> SetTodaysArrivalTime(DateTime? time, bool overRide)
            => TodaysArrivalTime == null ?
                Task.FromResult((true, TodaysArrivalTime = time).Item1)
                : (overRide == true ?
                Task.FromResult((true, TodaysArrivalTime = time, TodaysDepartureTime = null).Item1)
                : Task.FromResult(false));

        public Task<bool> SetTodaysDepartureTime(DateTime? time, bool overRide)
            => TodaysDepartureTime == null && TodaysArrivalTime.HasValue ?
                Task.FromResult((true, TodaysDepartureTime = time).Item1)
                : (overRide == true && TodaysArrivalTime.HasValue ?
                Task.FromResult((true, TodaysDepartureTime = time).Item1)
                : Task.FromResult(false));
        public Task<bool> SetExpectedArrivalTime(DateTime? time, bool overRide)
            => ExpectedArrivalTime == null ?
                Task.FromResult((true, ExpectedArrivalTime = time).Item1)
                : (overRide == true ?
                Task.FromResult((true, ExpectedArrivalTime = time, ExpectedDepartureTime = null).Item1)
                : Task.FromResult(false));

        public Task<bool> SetExptectedDepartureTime(DateTime? time, bool overRide)
            => ExpectedDepartureTime == null && ExpectedArrivalTime.HasValue ?
                Task.FromResult((true, ExpectedDepartureTime = time).Item1)
                : (overRide == true && ExpectedArrivalTime.HasValue ?
                Task.FromResult((true, ExpectedDepartureTime = time).Item1)
                : Task.FromResult(false));

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

        public Task<VisitorDTO> ConvertToVisitorDTO()
        {
            VisitorDTO visitorDTO = new()
            {
                Id = this.Id,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                LastName = this.LastName,
                ExpectedArrivalTime = this.ExpectedArrivalTime.ToString(),
                ExpectedDepartureTime = this.ExpectedArrivalTime.ToString(),
                TodaysArrivalTime = this.TodaysArrivalTime.ToString(),
                TodaysDepartureTime = this.TodaysDepartureTime.ToString()
            };

            return Task.FromResult(visitorDTO);
        }

        public Task ConvertFromDto(VisitorDTO visitorDTO)
        {
            Id = visitorDTO.Id;
            FirstName = visitorDTO.FirstName;
            MiddleName = visitorDTO.MiddleName;
            LastName = visitorDTO.LastName;
            ExpectedArrivalTime = visitorDTO.ExpectedArrivalTime.StringToNullableDateTime();
            ExpectedDepartureTime = visitorDTO.ExpectedDepartureTime.StringToNullableDateTime();
            TodaysArrivalTime = visitorDTO.TodaysArrivalTime.StringToNullableDateTime();
            TodaysDepartureTime = visitorDTO.TodaysDepartureTime.StringToNullableDateTime();
            FullName = string.IsNullOrEmpty(MiddleName) ? $"{FirstName} {LastName}" : $"{FirstName} {MiddleName} {LastName}";

            return Task.CompletedTask;
        }
    }
}
