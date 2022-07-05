using System.Text;
using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Commands;

public class CatCommand : ICommand
{
    public IEnumerable<string> Names { get; } = new[] { "cat" };

    public string Description => "concatenate files and print on the standard output.";

    public ValueTask InvokeAsync(IConsoleHost consoleHost, string[] args, CancellationToken cancellationToken)
    {
        const int bufferSize = 1024;
        Span<byte> buffer = stackalloc byte[bufferSize];

        foreach (var path in args.Skip(1))
        {
            if (!File.Exists(path)) { consoleHost.WriteLine($"cat: {path}: No such file or directory"); continue; }
            if (Directory.Exists(path)) { consoleHost.WriteLine($"cat: {path}: Is a directory"); continue; }
            using var stream = File.OpenRead(path);
            for (; ; )
            {
                var cbRead = stream.Read(buffer);
                if (cbRead == 0) break;
                var decodedText = Encoding.UTF8.GetString(buffer.Slice(0, cbRead));
                consoleHost.Write(decodedText);
                if (cbRead < bufferSize) break;
            }
        }
        return ValueTask.CompletedTask;
    }
}
