using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices
{
    public interface IMessageResponse
    {
        public string GetMessage(HttpResponseMessage response);
    }
}
