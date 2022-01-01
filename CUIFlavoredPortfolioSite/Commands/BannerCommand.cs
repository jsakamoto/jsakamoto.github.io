using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Commands;

public class BannerCommand : RainbowBannerCommandBase, ICommand
{
    public IEnumerable<string> Names { get; } = new[] { "banner" };

    public string Description => "show opening banner.";

    public void Invoke(IConsoleHost consoleHost, string[] args)
    {
        this.Render(consoleHost, "I'm");
        this.Render(consoleHost, "J.Sakamoto !");
    }
}
