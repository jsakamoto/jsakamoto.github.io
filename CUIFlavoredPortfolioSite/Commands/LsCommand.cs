using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;
using static Toolbelt.AnsiEscCode.Colorize;

namespace CUIFlavoredPortfolioSite.Commands;

public class LsCommand : ICommand
{
    public IEnumerable<string> Names { get; } = new[] { "ls" };

    public string Description => "list directory contents.";

    public void Invoke(IConsoleHost consoleHost, string[] args)
    {
        var targetDir = Environment.CurrentDirectory;
        var wildCard = "*.*";

        var dirs = Directory.GetDirectories(targetDir, wildCard)
            .Select(path => (IsDir: true, Name: Path.GetRelativePath(targetDir, path)));
        var files = Directory.GetFiles(targetDir, wildCard)
            .Select(path => (IsDir: false, Name: Path.GetRelativePath(targetDir, path)));
        var entries = dirs.Concat(files)
            .OrderBy(e => e.Name, StringComparer.Ordinal)
            .Select(e => e.IsDir ? Blue(e.Name) : e.Name);
        consoleHost.WriteLine(string.Join("  ", entries));
    }
}
