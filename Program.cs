using System;
using System.Net.Http;
using System.Threading.Tasks;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CUIFlavoredPortfolioSite
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IConsoleHost, ConsoleHostService>();

            await builder.Build().RunAsync();
        }
    }
}
