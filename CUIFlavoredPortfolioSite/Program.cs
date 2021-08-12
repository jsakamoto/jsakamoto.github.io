using CUIFlavoredPortfolioSite.Commands;
using CUIFlavoredPortfolioSite.Services;
using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace CUIFlavoredPortfolioSite;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");

        ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress);

        await builder.Build().RunAsync();
    }

    public static void ConfigureServices(IServiceCollection services, string baseAddress)
    {
        services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
        services.AddScoped<IConsoleHost, ConsoleHostService>();
        services.AddScoped<ICommandSet, CommandSetService>();
        services.AddScoped<CommandCompletion>();

        RegisterCommands(services);
    }

    private static void RegisterCommands(IServiceCollection services)
    {
        services.AddScoped<ICommand, ClearCommand>();
        services.AddScoped<ICommand, HelpCommand>();
        services.AddScoped<ICommand, FiggleCommand>();
        services.AddScoped<ICommand, ProfileCommand>();
        services.AddScoped<ICommand, BannerCommand>();
        services.AddScoped<ICommand, VersionCommand>();
        services.AddScoped<ICommand, RuntimeInformationCommand>();
    }
}
