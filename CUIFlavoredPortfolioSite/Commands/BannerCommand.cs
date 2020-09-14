using System;
using System.Collections.Generic;
using System.Linq;
using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;
using Figgle;
using static Toolbelt.AnsiEscCode.Colorize;

namespace CUIFlavoredPortfolioSite.Commands
{
    public class BannerCommand : ICommand
    {
        public IEnumerable<string> Names { get; } = new[] { "banner" };

        public string Description => "show opening banner.";

        private static readonly IReadOnlyList<Func<string, string>> _Colors = new Func<string, string>[] { DarkRed, Red, Yellow, Green, Cyan, DarkCyan, Blue, DarkBlue };

        public void Invoke(IConsoleHost consoleHost, string[] args)
        {
            Render(consoleHost, "I'm");
            Render(consoleHost, "J.Sakamoto !");
        }

        private void Render(IConsoleHost consoleHost, string text)
        {
            var lines = FiggleFonts.Slant.Render(text).Split('\n').Select(s => s.TrimEnd('\r')).ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                consoleHost.WriteLine(_Colors[i].Invoke(lines[i]));
            }
        }
    }
}
