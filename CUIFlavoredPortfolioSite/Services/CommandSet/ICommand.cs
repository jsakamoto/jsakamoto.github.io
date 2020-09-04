using System.Collections.Generic;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Services.CommandSet
{
    public interface ICommand
    {
        IEnumerable<string> Names { get; }

        void Invoke(IConsoleHost consoleHost, string[] args);
    }
}
