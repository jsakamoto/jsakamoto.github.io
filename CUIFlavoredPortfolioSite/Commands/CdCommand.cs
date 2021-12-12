using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Commands;

public class CdCommand : ICommand
{
    public IEnumerable<string> Names { get; } = new[] { "cd" };

    public string Description => "change the shell working directory.";

    public void Invoke(IConsoleHost consoleHost, string[] args)
    {
        if (args.Length < 2) return;
        var path = args[1];
        var fullPath = Path.GetFullPath(path);
        if (!Directory.Exists(fullPath))
        {
            consoleHost.WriteLine($"cd: {path}: No such file or directory");
            return;
        }
        Environment.CurrentDirectory = fullPath;
    }
}
