using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Commands;

public class PwdCommand : ICommand
{
    public IEnumerable<string> Names { get; } = new[] { "pwd" };

    public string Description => "print name of current/working directory";

    public ValueTask InvokeAsync(IConsoleHost consoleHost, string[] args, CancellationToken cancellationToken)
    {
        consoleHost.WriteLine(Environment.CurrentDirectory);
        return ValueTask.CompletedTask;
    }
}
