using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Client.ClientServices.IMessageResponseFolder;
using VisitorRegistrationV2.Blazor.Client.ClientServices.ISignalRCommandsFolder;
using VisitorRegistrationV2.Blazor.Shared;
using System.Net.Http;
using System.Net.Http.Json;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices.IHttpCommandsFolder
{
    public class CrudCommands : ICrudCommands
    {
        private ISignalRCommands commands;
        private IMessageResponse manager;
        protected HttpClient Http;
        public CrudCommands(ISignalRCommands commands, IMessageResponse manager, HttpClient Http)
        {
            this.commands = commands;
            this.manager = manager;
            this.Http = Http;
        }
        public async Task<Visitor> GetUpdatedUser(int visitorId)
        {
            Visitor FoundVisitor = await Http.GetFromJsonAsync<Visitor>($"api/Visitor/{visitorId}");

            return FoundVisitor;
        }

        public async Task<List<Visitor>> LoadVisitorOverViewItems()
        {
            return await Http.GetFromJsonAsync<List<Visitor>>("api/Visitor");
        }

        public async Task<string> saveNewVisitor(Visitor visitor)
        {
            if (commands.IsConnected())
            {
                string Message;
                Visitor addedVisitor;
                using var response = await Http.PostAsJsonAsync("api/Visitor", visitor);
                {
                    Message = manager.GetMessage(response);

                    addedVisitor = await response.Content.ReadFromJsonAsync<Visitor>();

                    await commands.SendAddedUserNotification(visitor.Id);
                }
                return Message;
            }
            else
            {
                return "Error no Connection";
            }
        }

        public async Task<string> VisitorArrived(Visitor visitorThatArrived)
        {
            string message;
            if (commands.IsConnected())
            { 
                using var response = await Http.PutAsJsonAsync($"api/visitor/{visitorThatArrived.Id}", visitorThatArrived);
                {
                    message = manager.GetMessage(response);

                    await commands.SendUpdatedUserNotification(visitorThatArrived.Id);
                }
                return message;
            }
            else
            {
                return "No Connection";
            }
        }

        public async Task<string> VisitorDeparted(Visitor visitorThatDeparted)
        {
            string message;
            if (commands.IsConnected())
            {
                using var response = await Http.PutAsJsonAsync($"api/visitor/{visitorThatDeparted.Id}", visitorThatDeparted);
                {
                    message = manager.GetMessage(response);

                    await commands.SendUpdatedUserNotification(visitorThatDeparted.Id);
                }
                return message;
            }
            else
            {
                return "No Connection";
            }
        }
    }
}
