using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandLineSwitchParser;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;
using Figgle;
using static Toolbelt.AnsiEscCode.Colorize;

namespace CUIFlavoredPortfolioSite.Services.CommandSet.Commands
{
    public class FiggleCommand : ICommand
    {
        public IEnumerable<string> Names { get; } = new[] { "figgle", "figlet" };

        public string Description => "ASCII banner generation";

        private class Options
        {
            public string Font { get; set; } = nameof(FiggleFonts.Standard);
            public bool Help { get; set; }
        }

        public class FontNotFoundException : Exception
        {
            public FontNotFoundException(string fontName) : base($"{fontName}: Unable to open font file.") { }
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

                var fontProp = typeof(FiggleFonts)
                    .GetProperties(BindingFlags.Static | BindingFlags.Public)
                    .Where(prop => prop.PropertyType == typeof(FiggleFont))
                    .FirstOrDefault(prop => prop.Name.ToUpper() == options.Font.ToUpper());
                if (fontProp == null) throw new FontNotFoundException(options.Font);

                var font = fontProp.GetValue(null) as FiggleFont;
                var bannerText = font.Render(string.Join(' ', args.Skip(1)));
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
        }
    }
}
