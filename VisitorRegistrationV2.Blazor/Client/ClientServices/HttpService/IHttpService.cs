using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices.HttpService
{
    public interface IHttpService
    {
        public Task<List<Visitor>> GetVisitorList();
        public Task<Visitor> GetVisitor(int id);
        public Task<HttpResponseMessage> UpdateVisitor(Visitor visitor);
        public Task<HttpResponseMessage> AddVisitor(Visitor visitor);
    }
}
