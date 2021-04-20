using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;
using VisitorRegistrationV2.Blazor.Shared.DTOs;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices
{
    public interface IHttpService
    {
        /// <summary>
        /// Sends http get request for full list.
        /// </summary>
        /// <returns></returns>
        public Task<List<VisitorDTO>> GetVisitorList();

        /// <summary>
        /// Sends http get by Id request.
        /// </summary>
        /// <param name="id">Id of requested visitor</param>
        /// <returns></returns>
        public Task<VisitorDTO> GetVisitor(int id);

        /// <summary>
        /// Sends put request by id and with all data in the body.
        /// </summary>
        /// <param name="visitor">Visitor to update</param>
        /// <returns></returns>
        public Task<HttpResponseMessage> UpdateVisitor(VisitorDTO visitor);

        /// <summary>
        /// Sends http post request with visitor as body.
        /// </summary>
        /// <param name="visitor">Visitor that needs to be added</param>
        /// <returns></returns>
        public Task<HttpResponseMessage> AddVisitor(VisitorDTO visitor);

        //public Task<List<Visitor>> SearchVisitor(string ServerSearchTerm);
    }
}
