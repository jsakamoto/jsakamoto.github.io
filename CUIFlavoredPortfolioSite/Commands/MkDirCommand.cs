using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Commands;

public class MkDirCommand : ICommand
{
    public IEnumerable<string> Names { get; } = new[] { "mkdir" };

    public string Description => "make directories.";

    public ValueTask InvokeAsync(IConsoleHost consoleHost, string[] args, CancellationToken cancellationToken)
    {
        if (args.Length < 2) { consoleHost.WriteLine("mkdir: missing operand"); return ValueTask.CompletedTask; }
        foreach (var path in args.Skip(1))
        {
            var fullPath = Path.GetFullPath(path);
            if (Directory.Exists(fullPath)) { consoleHost.WriteLine($"mkdir: cannot create directory ‘{path}’: File exists"); continue; }
            if (File.Exists(fullPath)) { consoleHost.WriteLine($"mkdir: cannot create directory ‘{path}’: File exists"); continue; }

            Directory.CreateDirectory(fullPath);
        }

        return ValueTask.CompletedTask;
    }
}
