using CUIFlavoredPortfolioSite.Commands.Helpers;
using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Commands;

public class RmCommand : ICommand
{
    public IEnumerable<string> Names { get; } = new[] { "rm" };

    public string Description => "remove files or directories";

    public class RmCommandOptions
    {
        public bool Force { get; set; }
        public bool Recursive { get; set; }
    }

    public async ValueTask InvokeAsync(IConsoleHost consoleHost, string[] args, CancellationToken cancellationToken)
    {
        args = args.Select(arg => arg.Trim()).Where(arg => arg != "").ToArray();

        if (!CommandOptionsParser.TryParse<RmCommandOptions>(ref args, out var options, out var errors))
        {
            consoleHost.WriteLine($"rm: {errors}");
            return;
        }

        if (args.Length < 2) { consoleHost.WriteLine("rm: missing operand"); return; }

        foreach (var path in args.Skip(1).SelectMany(arg => GetFileSystemEntries(arg, consoleHost, options)))
        {
            var pathIsDirectory = Directory.Exists(path);
            if (pathIsDirectory && !options.Recursive)
            {
                consoleHost.WriteLine($"rm: cannot remove '{path}': Is a directory");
                continue;
            }

            await RemoveAsync(path, pathIsDirectory, options, cancellationToken);
            if (cancellationToken.IsCancellationRequested) return;
        }
    }

    private static IEnumerable<string> GetFileSystemEntries(string path, IConsoleHost consoleHost, RmCommandOptions options)
    {
        var dir = Path.GetDirectoryName(path);
        dir = Path.GetFullPath(dir switch { "" => ".", null => "/", _ => dir });
        var fileName = Path.GetFileName(path);
        var fileSystemEntries = Directory.GetFileSystemEntries(dir, fileName);

        if (!fileSystemEntries.Any())
        {
            if (!options.Force) consoleHost.WriteLine($"rm: cannot remove '{path}': No such file or directory");
        }

        return fileSystemEntries;
    }

    private static async ValueTask RemoveAsync(string path, bool pathIsDirectory, RmCommandOptions options, CancellationToken cancellationToken)
    {
        await Task.Delay(1);

        if (!pathIsDirectory)
        {
            if (cancellationToken.IsCancellationRequested) return;
            try { File.Delete(path); }
            catch { if (!options.Force) throw; }
            return;
        }
        else
        {
            var subDirectories = Directory.GetDirectories(path);
            foreach (var subDirectory in subDirectories)
            {
                await RemoveAsync(subDirectory, pathIsDirectory: true, options, cancellationToken);
            }
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                await RemoveAsync(file, pathIsDirectory: false, options, cancellationToken);
            }
            if (cancellationToken.IsCancellationRequested) return;
            try { Directory.Delete(path); }
            catch { if (!options.Force) throw; }
        }
    }
}
