using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorRegistrationV2.Blazor.Client.ClientServices
{
    public interface ISignalRService
    {
        public event Action<int> NotifyOfUpdate;
        public event Action<int> NotifyOfAdded;
        public Task SendUpdateNotification(int visitorId);
        public Task SendAddNotification(int visitorId);
        public Task<bool> IsConnected();

    }
}
