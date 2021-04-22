using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistration.Blazor.Client.Models;
using VisitorRegistrationV2.Blazor.Shared;
using VisitorRegistrationV2.Blazor.Shared.DTOs;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices
{
    public class VisitorService : IVisitorService
    {
        ISignalRService signalRService;
        IHttpService Http;
        IMessageResponse ResponseManager;

        public VisitorService(ISignalRService signalRService, IHttpService Http, IMessageResponse ResponseManager)
        {
            this.signalRService = signalRService;
            this.Http = Http;
            this.ResponseManager = ResponseManager;
        }

        private string Message { get; set; }
        string IVisitorService.Message { get => Message; set => Message = value; }

        public void MessageDisposal()
        {
            Message = "";
        }

        public async Task<ClientVisitor> VisitorArrives(ClientVisitor visitorThatArrived, bool OverRide, DateTime? time)
        {
            if (await signalRService.IsConnected())
            {
                if (time == null)
                {
                    time = DateTime.Now;
                }

                bool Succeeded = await visitorThatArrived.SetTodaysArrivalTime(time, OverRide);

                if (Succeeded)
                {
                    var visitorDTO = await visitorThatArrived.ConvertToVisitorDTO();

                    var response = await Http.UpdateVisitor(visitorDTO);

                    await signalRService.SendUpdatedVisitorNotification(visitorDTO.Id);

                    visitorThatArrived = new(visitorDTO);

                    Message = ResponseManager.GetMessage(response);
                }
            }
            else
            {
                Message = "No Connection";
            }

            return visitorThatArrived;
        }

        public async Task<ClientVisitor> VisitorDeparts(ClientVisitor visitorThatDeparted, bool OverRide, DateTime? time)
        {
            if (await signalRService.IsConnected())
            {
                if (time == null)
                {
                    time = DateTime.Now;
                }

                var Succeeded = await visitorThatDeparted.SetTodaysDepartureTime(time, OverRide);

                if (Succeeded)
                {
                    var visitorDTO = await visitorThatDeparted.ConvertToVisitorDTO();

                    var response = await Http.UpdateVisitor(visitorDTO);

                    await signalRService.SendUpdatedVisitorNotification(visitorThatDeparted.Id);

                    Message = ResponseManager.GetMessage(response);

                    visitorThatDeparted = new(visitorDTO);
                }
            }
            else
            {
                Message = "No Connection";
            }

            return visitorThatDeparted;
        }
    }
}
