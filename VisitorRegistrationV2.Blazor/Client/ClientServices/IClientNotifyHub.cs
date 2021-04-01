using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices
{
    public interface IClientNotifyHub
    {
        Task SendUpdatedVisitorNotify(int visitorId);
        Task SendAddedVisitorNotify(int visitorId);
    }
}
