using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorRegistrationV2.Blazor.Server.CustomLogger
{
    public class ConsoleLogger : ILogger
    {
        private readonly string _name;
        private readonly ConsoleLoggerConfig _config;

        public ConsoleLogger(string name, ConsoleLoggerConfig config) => (_name, _config) = (name, config);

        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel) => _config.LogLevels.ContainsKey(logLevel);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (_config.EventID == 0 || _config.EventID == eventId.Id)
            {
                ConsoleColor orginalColor = Console.ForegroundColor;

                Console.ForegroundColor = _config.LogLevels[logLevel];
                Console.WriteLine($"[{eventId.Id,2}: {logLevel,-12}]");

                Console.ForegroundColor = orginalColor;
                Console.WriteLine($"      {_name} - {formatter(state, exception)}");
            }
        }
    }
}
