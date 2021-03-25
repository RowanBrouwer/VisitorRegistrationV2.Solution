using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices.IMessageResponseFolder
{
    public class MessageResponse : IMessageResponse
    {
        public string GetMessage(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return ("Action succesfull!");
                case HttpStatusCode.NoContent:
                    return ("Action succesfull!");
                case HttpStatusCode.InternalServerError:
                    return ("Oops something went wrong with the server!");
                case HttpStatusCode.NotFound:
                    return ("The requested resource was not found.");
                case HttpStatusCode.Conflict:
                    return ("The requested action has caused a conflict on the server.");
                case HttpStatusCode.BadRequest:
                    return ("Bad Request");
                case HttpStatusCode.Accepted:
                    return ("The requested action has been accepted");
                default:
                    return ("");
            }
        }
    }
}
