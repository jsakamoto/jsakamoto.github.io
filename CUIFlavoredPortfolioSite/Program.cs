﻿using CUIFlavoredPortfolioSite;
using CUIFlavoredPortfolioSite.Services;
using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;
using CUIFlavoredPortfolioSite.Services.ScreenMetrics;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress);
await builder.Build().RunAsync();

static void ConfigureServices(IServiceCollection services, string baseAddress)
{
    services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
    services.AddScoped<IScreenMetrics, ScreenMetricsService>();
    services.AddScoped<IConsoleHost, ConsoleHostService>();
    services.AddScoped<ICommandSet, CommandSetService>();
    services.AddScoped<CommandCompletion>();
    services.AddHotKeys2();
    services.AddPWAUpdater();

    RegisterCommands(services);
}

static void RegisterCommands(IServiceCollection services)
{
    var commandTypes = typeof(Program).Assembly
        .GetTypes()
        .Where(t => t.GetInterfaces().Contains(typeof(ICommand)));
    foreach (var commandType in commandTypes)
    {
        services.AddScoped(typeof(ICommand), commandType);
    }
}
