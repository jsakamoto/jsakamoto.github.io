using CUIFlavoredPortfolioSite.Services.ConsoleHost;
using Figgle;
using static Toolbelt.AnsiEscCode.Colorize;

namespace CUIFlavoredPortfolioSite.Commands;

public class RainbowBannerCommandBase
{
    protected static readonly IReadOnlyList<Func<string, string>> _Colors = new Func<string, string>[] { DarkRed, Red, Yellow, Green, Cyan, DarkCyan, Blue, DarkBlue };

    protected void Render(IConsoleHost consoleHost, string text)
    {
        var lines = FiggleFonts.Slant.Render(text).Split('\n').Select(s => s.TrimEnd('\r')).ToArray();
        for (int i = 0; i < lines.Length; i++)
        {
            consoleHost.WriteLine(_Colors[i].Invoke(lines[i]));
        }
    }
}
