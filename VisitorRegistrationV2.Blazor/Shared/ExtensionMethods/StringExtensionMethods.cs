using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorRegistrationV2.Blazor.Shared.ExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static DateTime? StringToNullableDateTime(this string str) 
            => DateTime.TryParse(str, out var date) ? date : null;
    }
}
