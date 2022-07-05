using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Commands;

public class BannerCommand : RainbowBannerCommandBase, ICommand
{
    public IEnumerable<string> Names { get; } = new[] { "banner" };

    public string Description => "show opening banner.";

    public ValueTask InvokeAsync(IConsoleHost consoleHost, string[] args, CancellationToken cancellationToken)
    {
        this.Render(consoleHost, "I'm");
        this.Render(consoleHost, "J.Sakamoto !");
        return ValueTask.CompletedTask;
    }
}
