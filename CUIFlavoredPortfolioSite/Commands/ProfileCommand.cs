using System.Collections.Generic;
using System.Linq;
using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;
using static Toolbelt.AnsiEscCode.Colorize;

namespace CUIFlavoredPortfolioSite.Commands
{
    public class ProfileCommand : ICommand
    {
        public IEnumerable<string> Names { get; } = new[] { "profile" };

        public string Description => "show profile about me.";

        public void Invoke(IConsoleHost consoleHost, string[] args)
        {
            if (args.Skip(1).Any())
            {
                consoleHost.WriteLine($"Usage: {args[0]}");
                return;
            }

            consoleHost.WriteLine(Cyan("Name") + "      - J.Sakamoto");
            consoleHost.WriteLine(Cyan("Location") + "  - Sapporo, Hokkaido, Japan");

            consoleHost.WriteLine();

            consoleHost.WriteLine(Cyan("Biography") + " -");
            consoleHost.WriteLine("I'm a programmer since before 30 years over.");
            consoleHost.WriteLine("My most favorite technical area is building a Web application built on .NET technology.");
            consoleHost.WriteLine("I have been used C# (with Blazor, ASP.NET Core, EFCore) and TypeScript (with Angular) on Visual Studio and Windows OS for creating my products.");
            consoleHost.WriteLine("I also publish numerous NuGet packages on the nuget.org as an open-source.");

            consoleHost.WriteLine();

            consoleHost.WriteLine($"{Cyan("Twitter")} - {DarkCyan("[@jsakamoto](https://twitter.com/jsakamoto)")}");
            consoleHost.WriteLine($"{Cyan("GitHub ")} - {DarkCyan("[https://github.com/jsakamoto](https://github.com/jsakamoto)")}");
            consoleHost.WriteLine($"{Cyan("NuGet  ")} - {DarkCyan("[https://www.nuget.org/profiles/jsakamoto](https://www.nuget.org/profiles/jsakamoto)")}");
            consoleHost.WriteLine($"{Cyan("Blogs  ")} -");
            consoleHost.WriteLine($" - dev.to      - {DarkCyan("[https://dev.to/j_sakamoto](https://dev.to/j_sakamoto)")} {DarkGray(" (English contents)")}");
            consoleHost.WriteLine($" - Qiita       - {DarkCyan("[https://qiita.com/jsakamoto](https://qiita.com/jsakamoto)")} {DarkGray(" (Japanese contents)")}");
            consoleHost.WriteLine($" - excite blog - {DarkCyan("[https://devadjust.exblog.jp/](https://devadjust.exblog.jp/)")} {DarkGray(" (Japanese contents)")}");
        }
    }
}
