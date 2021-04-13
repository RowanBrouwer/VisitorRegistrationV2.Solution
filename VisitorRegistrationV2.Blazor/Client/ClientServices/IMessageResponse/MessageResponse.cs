using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices
{
    public class MessageResponse : IMessageResponse
    {
        ILogger<MessageResponse> logger;
        public MessageResponse(ILogger<MessageResponse> logger)
        {
            this.logger = logger;       
        }
        /// <summary>
        /// Returns a string based on the HttpResponseMessage given to it.
        /// </summary>
        /// <param name="response">HttpResponseMessage to get the statuscode from</param>
        /// <returns></returns>
        public string GetMessage(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    logger.LogInformation($"{response.StatusCode}");
                    return ("Action succesfull!");
                case HttpStatusCode.NoContent:
                    logger.LogInformation($"{response.StatusCode}");
                    return ("Action succesfull!");
                case HttpStatusCode.InternalServerError:
                    logger.LogInformation($"{response.StatusCode}");
                    return ("Oops something went wrong with the server!");
                case HttpStatusCode.NotFound:
                    logger.LogInformation($"{response.StatusCode}");
                    return ("The requested resource was not found.");
                case HttpStatusCode.Conflict:
                    logger.LogInformation($"{response.StatusCode}");
                    return ("The requested action has caused a conflict on the server.");
                case HttpStatusCode.BadRequest:
                    logger.LogInformation($"{response.StatusCode}");
                    return ("Bad Request");
                case HttpStatusCode.Accepted:
                    logger.LogInformation($"{response.StatusCode}");
                    return ("The requested action has been accepted");
                default:
                    logger.LogInformation($"{response.StatusCode}");
                    return ("Uknown Error");
            }
        }
    }
}
