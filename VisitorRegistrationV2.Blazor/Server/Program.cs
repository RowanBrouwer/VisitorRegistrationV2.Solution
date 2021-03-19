using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Shared;
using VisitorRegistrationV2.Data;

namespace VisitorRegistrationV2.Blazor.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var db = serviceProvider.GetRequiredService<ApplicationDbContext>();

                    var userManager = serviceProvider.
                        GetRequiredService<UserManager<Registrar>>();

                    ApplicationSeed.Seed
                        (userManager, db, false);
                }
                catch
                {

                }
            }
            host.Run();
        }
    

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
