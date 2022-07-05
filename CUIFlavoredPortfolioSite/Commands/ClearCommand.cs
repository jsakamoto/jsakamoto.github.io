using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Commands;

public class ClearCommand : ICommand
{
    public IEnumerable<string> Names { get; } = new[] { "clear", "cls" };

    public string Description => "clear console.";

    public ValueTask InvokeAsync(IConsoleHost consoleHost, string[] args, CancellationToken cancellationToken)
    {
        if (args.Skip(1).Any())
            consoleHost.WriteLine($"Usage: {args[0]}");
        else
            consoleHost.Clear();

        return ValueTask.CompletedTask;
    }
}
