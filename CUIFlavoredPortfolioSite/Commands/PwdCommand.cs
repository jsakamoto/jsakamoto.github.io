using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Commands;

public class PwdCommand : ICommand
{
    public IEnumerable<string> Names { get; } = new[] { "pwd" };

    public string Description => "print name of current/working directory";

    public void Invoke(IConsoleHost consoleHost, string[] args)
    {
        consoleHost.WriteLine(Environment.CurrentDirectory);
    }
}
