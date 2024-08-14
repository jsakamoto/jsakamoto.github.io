using CUIFlavoredPortfolioSite.Services;
using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Commands;

public class CdCommand(PathUtility pathUtility) : ICommand
{
    public IEnumerable<string> Names { get; } = new[] { "cd" };

    public string Description => "change the shell working directory.";

    public ValueTask InvokeAsync(IConsoleHost consoleHost, string[] args, CancellationToken cancellationToken)
    {
        if (args.Length < 2) return ValueTask.CompletedTask;
        var path = args[1];
        var fullPath = Path.GetFullPath(pathUtility.RevertUserHomePath(path));
        if (!Directory.Exists(fullPath))
        {
            consoleHost.WriteLine($"cd: {path}: No such file or directory");
            return ValueTask.CompletedTask;
        }
        Environment.CurrentDirectory = fullPath;

        return ValueTask.CompletedTask;
    }
}
