using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VisitorRegistrationV2.Blazor.Client.ClientServices;

namespace VisitorRegistrationV2.Blazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("VisitorRegistrationV2.Blazor.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddApiAuthorization();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("VisitorRegistrationV2.Blazor.ServerAPI"));

            builder.Services.AddScoped<IMessageResponse, MessageResponse>();

            builder.Services.AddScoped<IHttpService, HttpService>();
            builder.Services.AddSingleton<ISignalRService, SignalRService>();
            builder.Services.AddScoped<IVisitorService ,VisitorService>();

            await builder.Build().RunAsync();
        }
    }
}
