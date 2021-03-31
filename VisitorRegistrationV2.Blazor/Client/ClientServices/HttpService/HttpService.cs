using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices.HttpService
{
    public class HttpService : IHttpService
    {
        protected HttpClient Http;
        public HttpService(HttpClient Http)
        {
            this.Http = Http;
        }

        public async Task<HttpResponseMessage> AddVisitor(Visitor visitor)
        {
            var result = await Http.PostAsJsonAsync("api/Visitor", visitor);

            return result;
        }

        public async Task<Visitor> GetVisitor(int id)
        { 
            var result = await Http.GetFromJsonAsync<Visitor>($"api/Visitor/{id}");

            return result;
        }

        public async Task<List<Visitor>> GetVisitorList()
        {
            var result = await Http.GetFromJsonAsync<List<Visitor>>("api/Visitor");

            return result;
        }


        public async Task<HttpResponseMessage> UpdateVisitor(Visitor visitor)
        {
            var result = await Http.PutAsJsonAsync($"api/visitor/{visitor.Id}", visitor);

            return result;
        }
    }
}
