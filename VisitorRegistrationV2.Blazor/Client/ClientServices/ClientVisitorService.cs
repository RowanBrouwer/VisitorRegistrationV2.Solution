using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices
{
    public class ClientVisitorService
    {
        SignalRService signalRService;
        IHttpService Http;
        IMessageResponse ResponseManager;

        public ClientVisitorService(SignalRService signalRService, IHttpService Http, IMessageResponse ResponseManager)
        {
            this.signalRService = signalRService;
            this.Http = Http;
            this.ResponseManager = ResponseManager;
        }

        public string Message { get; set; }

        public void MessageDisposal()
        {
            Message = "";
        }

        public async Task<Visitor> VisitorArrives(Visitor visitorThatArrived, bool OverRide, DateTime? time)
        {
            if (signalRService.IsConnected)
            {
                var Succeeded = await visitorThatArrived.SetArrivalTime(time, OverRide);

                if (Succeeded)
                {
                    var response = await Http.UpdateVisitor(visitorThatArrived);

                    await signalRService.SendUpdateNotification(visitorThatArrived.Id);

                    Message = ResponseManager.GetMessage(response);
                }
            }
            else
            {
                Message = "No Connection";
            }

            return visitorThatArrived;
        }

        public async Task<Visitor> VisitorDeparts(Visitor visitorThatDeparted, bool OverRide, DateTime? time)
        {
            if (signalRService.IsConnected)
            {
                var Succeeded = await visitorThatDeparted.SetDepartureTime(time, OverRide);

                if (Succeeded)
                {
                    var response = await Http.UpdateVisitor(visitorThatDeparted);

                    await signalRService.SendUpdateNotification(visitorThatDeparted.Id);

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
