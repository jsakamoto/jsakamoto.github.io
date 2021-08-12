using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;
using static Toolbelt.AnsiEscCode.Colorize;

namespace CUIFlavoredPortfolioSite.Commands;

public class HelpCommand : ICommand
{
    public IEnumerable<string> Names { get; } = new[] { "help" };

    public string Description => "show this.";

    private readonly IServiceProvider _ServiceProvider;

    public HelpCommand(IServiceProvider serviceProvider)
    {
        this._ServiceProvider = serviceProvider;
    }

    public void Invoke(IConsoleHost consoleHost, string[] args)
    {
        if (args.Skip(1).Any())
            consoleHost.WriteLine($"Usage: {args[0]}");
        else
        {
            var commands = this._ServiceProvider.GetServices<ICommand>()
                .OrderBy(cmd => cmd.Names.First())
                .Select(cmd => (Names: string.Join(", ", cmd.Names), cmd.Description))
                .ToArray();
            var maxWidthCmdNames = commands.Max(maxWidthCmdNames => maxWidthCmdNames.Names.Length);

            foreach (var command in commands)
            {
                consoleHost.WriteLine($"{Cyan(command.Names.PadRight(maxWidthCmdNames))} {DarkGray("...")} {command.Description}");
            }
        }
    }
}
