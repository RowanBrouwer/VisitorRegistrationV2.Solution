using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Configuration;

namespace VisitorRegistrationV2.Blazor.Server.CustomLogger
{
    public static class ConsoleLoggerExtensions
    {
        public static ILoggingBuilder AddConsoleLogger(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, ConsoleLoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions
                <ConsoleLoggerConfig, ConsoleLoggerProvider>(builder.Services);

            return builder;
        }

        public static ILoggingBuilder AddConsoleLogger(this ILoggingBuilder builder, Action<ConsoleLoggerConfig> configure)
        {
            builder.AddConsoleLogger();
            builder.Services.Configure(configure);

            return builder;
        }

    }
}
