using System.Text;
using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Commands;

public class CatCommand : ICommand
{
    public IEnumerable<string> Names { get; } = new[] { "cat" };

    public string Description => "concatenate files and print on the standard output.";

    public async ValueTask InvokeAsync(IConsoleHost consoleHost, string[] args, CancellationToken cancellationToken)
    {
        foreach (var path in args.Skip(1))
        {
            if (!File.Exists(path)) { consoleHost.WriteLine($"cat: {path}: No such file or directory"); continue; }
            if (Directory.Exists(path)) { consoleHost.WriteLine($"cat: {path}: Is a directory"); continue; }
            using var stream = File.OpenRead(path);
            for (; ; )
            {
                var shouldBeContinue = ProcessOneLine(stream, consoleHost, cancellationToken);
                if (!shouldBeContinue) break;
                await Task.Delay(1);
            }
        }
    }

    private static bool ProcessOneLine(Stream stream, IConsoleHost consoleHost, CancellationToken cancellationToken)
    {
        const int bufferSize = 1024;
        Span<byte> buffer = stackalloc byte[bufferSize];
        var cbRead = stream.Read(buffer);
        if (cbRead == 0) return false;

        var decodedText = Encoding.UTF8.GetString(buffer.Slice(0, cbRead));
        consoleHost.Write(decodedText);

        if (cancellationToken.IsCancellationRequested) return false;
        if (cbRead < bufferSize) return false;

        return true;
    }
}
