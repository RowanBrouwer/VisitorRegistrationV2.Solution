using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorRegistrationV2.Blazor.Server.CustomLogger
{
    public class ConsoleLoggerConfig
    {
        public int EventID { get; set; }

        public Dictionary<LogLevel, ConsoleColor> LogLevels { get; set; } = new()
        {
            [LogLevel.Information] = ConsoleColor.Green,
            [LogLevel.Warning] = ConsoleColor.Yellow,
            [LogLevel.Critical] = ConsoleColor.Magenta,
            [LogLevel.Trace] = ConsoleColor.Cyan,
            [LogLevel.Error] = ConsoleColor.Red
        };
    }
}
