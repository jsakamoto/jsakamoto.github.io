using System.Collections.Generic;
using System.Linq;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Services.CommandSet.Commands
{
    public class ClearCommand : ICommand
    {
        public IEnumerable<string> Names { get; } = new[] { "clear", "cls" };

        public void Invoke(IConsoleHost consoleHost, string[] args)
        {
            if (args.Skip(1).Any())
                consoleHost.WriteLine($"Usage: {args[0]}");
            else
                consoleHost.Clear();
        }
    }
}
