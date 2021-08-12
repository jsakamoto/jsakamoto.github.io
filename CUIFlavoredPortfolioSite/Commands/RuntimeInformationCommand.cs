using System.Runtime.InteropServices;
using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;
using static Toolbelt.AnsiEscCode.Colorize;

namespace CUIFlavoredPortfolioSite.Commands;

public class RuntimeInformationCommand : ICommand
{
    public IEnumerable<string> Names { get; } = new[] { "runtimeinformation" };

    public string Description => "show runtime information of this app.";

    public void Invoke(IConsoleHost consoleHost, string[] args)
    {
        consoleHost.WriteLine($"{Cyan("Framework Description")}  - {RuntimeInformation.FrameworkDescription}");
        consoleHost.WriteLine($"{Cyan("Process Architecture")}   - {RuntimeInformation.ProcessArchitecture}");
        consoleHost.WriteLine($"{Cyan("OS Architecture")}        - {RuntimeInformation.OSArchitecture}");
        consoleHost.WriteLine($"{Cyan("OS Description")}         - {RuntimeInformation.OSDescription}");
        consoleHost.WriteLine($"{Cyan("OS Platform")}            - {Environment.OSVersion.Platform}");
        consoleHost.WriteLine($"{Cyan("OS Version")}             - {Environment.OSVersion.Version}");
        consoleHost.WriteLine($"{Cyan("OS Version ServicePack")} - {Environment.OSVersion.ServicePack}");
    }
}
