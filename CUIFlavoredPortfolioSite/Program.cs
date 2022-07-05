using CUIFlavoredPortfolioSite;
using CUIFlavoredPortfolioSite.Commands;
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
    services.AddHotKeys();

    RegisterCommands(services);
}

static void RegisterCommands(IServiceCollection services)
{
    services.AddScoped<ICommand, ClearCommand>();
    services.AddScoped<ICommand, HelpCommand>();
    services.AddScoped<ICommand, FiggleCommand>();
    services.AddScoped<ICommand, ProfileCommand>();
    services.AddScoped<ICommand, BannerCommand>();
    services.AddScoped<ICommand, NewYearGreetingCommand>();
    services.AddScoped<ICommand, VersionCommand>();
    services.AddScoped<ICommand, RuntimeInformationCommand>();
    services.AddScoped<ICommand, PwdCommand>();
    services.AddScoped<ICommand, CdCommand>();
    services.AddScoped<ICommand, LsCommand>();
    services.AddScoped<ICommand, MkDirCommand>();
    services.AddScoped<ICommand, CatCommand>();
    services.AddScoped<ICommand, PrintEnvCommand>();
    services.AddScoped<ICommand, SlCommand>();
}
