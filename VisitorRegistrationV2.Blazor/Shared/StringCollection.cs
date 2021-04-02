using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorRegistrationV2.Blazor.Shared
{
    public class StringCollection
    {
        private static string BaseUri = "https://localhost:44348/";

        public static string ApiUri = "api/Visitor";

        public static string HubUri = "/visitorhub";

        public static string VisitorUpdatedString = "UpdateVisitor";
        public static string VisitorAddedString = "AddedVisitor";

        public static string SendUpdatedVisitorNotificationString = "SendUpdateNotification";
        public static string SendAddedVisitorNotificationString = "SendAddNotification";
    }
}
