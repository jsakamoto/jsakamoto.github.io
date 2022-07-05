using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Commands;

public class NewYearGreetingCommand : RainbowBannerCommandBase, ICommand
{
    public IEnumerable<string> Names { get; } = new[] { "new-year-greeting" };

    public string Description => "show new year greeting.";

    public ValueTask InvokeAsync(IConsoleHost consoleHost, string[] args, CancellationToken cancellationToken)
    {
        this.Render(consoleHost, "Happy");
        this.Render(consoleHost, $"New Year {DateTime.Now.Year} !");
        return ValueTask.CompletedTask;
    }
}
