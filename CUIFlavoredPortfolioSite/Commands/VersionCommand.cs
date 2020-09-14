using System.Collections.Generic;
using System.Reflection;
using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;
using static Toolbelt.AnsiEscCode.Colorize;

namespace CUIFlavoredPortfolioSite.Commands
{
    public class VersionCommand : ICommand
    {
        public IEnumerable<string> Names { get; } = new[] { "version", "ver" };

        public string Description => "show version information of this site.";

        public void Invoke(IConsoleHost consoleHost, string[] args)
        {
            var assembly = this.GetType().Assembly;
            var version = assembly.GetName().Version;
            var productName = assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
            var copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;

            consoleHost.WriteLine($"{Yellow(productName)}");
            consoleHost.WriteLine($"{Cyan("Version")}   - {version}");
            consoleHost.WriteLine($"{Cyan("Copyright")} - {copyright}");
        }
    }
}
