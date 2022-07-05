using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Services.CommandSet;

public interface ICommand
{
    IEnumerable<string> Names { get; }

    string Description { get; }

    ValueTask InvokeAsync(IConsoleHost consoleHost, string[] args, CancellationToken cancellationToken);
}
