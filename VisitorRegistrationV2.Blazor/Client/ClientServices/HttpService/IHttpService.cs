using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices
{
    public interface IHttpService
    {
        /// <summary>
        /// Sends http get request for full list.
        /// </summary>
        /// <returns></returns>
        public Task<List<Visitor>> GetVisitorList();

        /// <summary>
        /// Sends http get by Id request.
        /// </summary>
        /// <param name="id">Id of requested visitor</param>
        /// <returns></returns>
        public Task<Visitor> GetVisitor(int id);

        /// <summary>
        /// Sends put request by id and with all data in the body.
        /// </summary>
        /// <param name="visitor">Visitor to update</param>
        /// <returns></returns>
        public Task<HttpResponseMessage> UpdateVisitor(Visitor visitor);

        /// <summary>
        /// Sends http post request with visitor as body.
        /// </summary>
        /// <param name="visitor">Visitor that needs to be added</param>
        /// <returns></returns>
        public Task<HttpResponseMessage> AddVisitor(Visitor visitor);
    }
}
