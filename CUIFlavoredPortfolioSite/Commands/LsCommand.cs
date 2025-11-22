using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;
using static Toolbelt.AnsiEscCode.Colorize;

namespace CUIFlavoredPortfolioSite.Commands;

public class LsCommand : ICommand
{
    public IEnumerable<string> Names { get; } = new[] { "ls" };

    public string Description => "list directory contents.";

    public ValueTask InvokeAsync(IConsoleHost consoleHost, string[] args, CancellationToken cancellationToken)
    {
        var pathCollection = args.Skip(1).Where(p => !string.IsNullOrEmpty(p));
        if (!pathCollection.Any()) pathCollection = pathCollection.Append(Path.Combine(Environment.CurrentDirectory, "*.*"));

        var multiEntries = pathCollection.Count() > 1;
        var firstEntry = true;

        foreach (var path in pathCollection)
        {
            var fullPath = Path.GetFullPath(path);
            if (Directory.Exists(fullPath)) { fullPath = Path.Combine(fullPath, "*.*"); }
            var targetDir = Path.GetDirectoryName(fullPath) ?? "";
            var wildCard = Path.GetFileName(fullPath);

            var dirs = Directory.GetDirectories(targetDir, wildCard)
                .Select(path => (IsDir: true, Name: Path.GetRelativePath(targetDir, path)));
            var files = Directory.GetFiles(targetDir, wildCard)
                .Select(path => (IsDir: false, Name: Path.GetRelativePath(targetDir, path)));
            var entries = dirs.Concat(files)
                .OrderBy(e => e.Name, StringComparer.Ordinal)
                .Select(e => e.IsDir ? Blue(e.Name) : e.Name)
                .ToArray();

            if (!firstEntry) consoleHost.WriteLine();
            if (multiEntries) consoleHost.WriteLine($"{path}:");

            if (!entries.Any() && !wildCard.Contains('*') && !wildCard.Contains('?')) { consoleHost.WriteLine($"ls: cannot access '{path}': No such file or directory"); }
            else { consoleHost.WriteLine(string.Join("  ", entries)); }

            firstEntry = false;
        }

        return ValueTask.CompletedTask;
    }
}
