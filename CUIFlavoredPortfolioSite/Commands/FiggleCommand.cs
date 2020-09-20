using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandLineSwitchParser;
using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;
using Figgle;
using static Toolbelt.AnsiEscCode.Colorize;

namespace CUIFlavoredPortfolioSite.Commands
{
    public class FiggleCommand : ICommand
    {
        public IEnumerable<string> Names { get; } = new[] { "figgle", "figlet" };

        public string Description => "ASCII banner generation";

        private readonly Lazy<IReadOnlyDictionary<string, Func<FiggleFont>>> _Fonts = new Lazy<IReadOnlyDictionary<string, Func<FiggleFont>>>(GetFonts);

        private class Options
        {
            public string Font { get; set; } = nameof(FiggleFonts.Standard);
            public bool Help { get; set; }
        }

        public class FontNotFoundException : Exception
        {
            public FontNotFoundException(string fontName) : base($"{fontName}: Unable to open font file. (to show available font names, try -h option.)") { }
        }

        public void Invoke(IConsoleHost consoleHost, string[] args)
        {
            var commandName = args[0];
            try
            {
                var options = CommandLineSwitch.Parse<Options>(ref args);
                if (args.Skip(1).Any() == false || options.Help)
                {
                    Usage(consoleHost, commandName);
                    return;
                }

                var found = _Fonts.Value.TryGetValue(options.Font.ToLower(), out var getFont);
                if (!found) throw new FontNotFoundException(options.Font);

                var bannerText = getFont().Render(string.Join(' ', args.Skip(1)));
                consoleHost.WriteLine(bannerText);
            }

            catch (FontNotFoundException e) { consoleHost.WriteLine(Yellow(e.Message)); }
            catch (InvalidCommandLineSwitchException e)
            {
                consoleHost.WriteLine(Yellow(e.Message));
                Usage(consoleHost, commandName);
            }
        }

        private void Usage(IConsoleHost consoleHost, string commandName)
        {
            consoleHost.WriteLine($"Usage: {commandName} [-f <font name>] [-h] <message>");
            consoleHost.WriteLine("  Available font names:");
            foreach (var fontName in _Fonts.Value.Keys.OrderBy(name => name))
            {
                consoleHost.WriteLine($"  - {fontName}");
            }
        }

        private static IReadOnlyDictionary<string, Func<FiggleFont>> GetFonts()
        {
            return typeof(FiggleFonts)
                .GetProperties(BindingFlags.Static | BindingFlags.Public)
                .Where(prop => prop.PropertyType == typeof(FiggleFont))
                .ToDictionary(prop => prop.Name.ToLower(), prop => (Func<FiggleFont>)(() => prop.GetValue(null) as FiggleFont));
        }
    }
}
