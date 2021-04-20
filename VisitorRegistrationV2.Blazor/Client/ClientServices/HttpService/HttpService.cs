using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;
using VisitorRegistrationV2.Blazor.Shared.DTOs;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices
{
    public class HttpService : IHttpService
    {
        protected HttpClient Http;
        ILogger<HttpService> logger;
        public HttpService(HttpClient Http, ILogger<HttpService> logger)
        {
            this.logger = logger;
            this.Http = Http;
        }

        public async Task<HttpResponseMessage> AddVisitor(VisitorDTO visitor)
        {
            logger.LogInformation($"Calling API-POST for Visitor {visitor.Id} at {DateTime.Now.ToShortTimeString()}");
            var result = await Http.PostAsJsonAsync(StringCollection.ApiUri, visitor);
            
            return result;
        }

        public async Task<VisitorDTO> GetVisitor(int id)
        {
            logger.LogInformation($"Calling API-GET for Visitor {id} at {DateTime.Now.ToShortTimeString()}");
            var result = await Http.GetFromJsonAsync<VisitorDTO>(StringCollection.ApiUri + $"/{id}");

            return result;
        }
        
        public async Task<List<VisitorDTO>> GetVisitorList()
        {
            logger.LogInformation($"Calling API-GET for Visitor List at {DateTime.Now.ToShortTimeString()}");
            var result = await Http.GetFromJsonAsync<List<VisitorDTO>>(StringCollection.ApiUri);

            return result;
        }

        public async Task<HttpResponseMessage> UpdateVisitor(VisitorDTO visitor)
        {
            logger.LogInformation($"Calling API-PUT for Visitor {visitor.Id} at {DateTime.Now.ToShortTimeString()}");
            var result = await Http.PutAsJsonAsync(StringCollection.ApiUri + $"/{visitor.Id}", visitor);

            return result;
        }
    }
}
