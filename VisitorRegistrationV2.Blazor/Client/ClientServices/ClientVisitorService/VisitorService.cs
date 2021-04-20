﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<VisitorDTO> VisitorArrives(VisitorDTO visitorThatArrived, bool OverRide, DateTime? time)
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
                    var response = await Http.UpdateVisitor(visitorThatArrived);

                    await signalRService.SendUpdatedVisitorNotification(visitorThatArrived.Id);

                    Message = ResponseManager.GetMessage(response);
                }
            }
            else
            {
                Message = "No Connection";
            }

            return visitorThatArrived;
        }

        public async Task<VisitorDTO> VisitorDeparts(VisitorDTO visitorThatDeparted, bool OverRide, DateTime? time)
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
                    var response = await Http.UpdateVisitor(visitorThatDeparted);

                    await signalRService.SendUpdatedVisitorNotification(visitorThatDeparted.Id);

                    Message = ResponseManager.GetMessage(response);
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
