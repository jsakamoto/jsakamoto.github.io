using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Services.CommandSet;

public interface ICommand
{
    IEnumerable<string> Names { get; }

    string Description { get; }

    void Invoke(IConsoleHost consoleHost, string[] args);
}
