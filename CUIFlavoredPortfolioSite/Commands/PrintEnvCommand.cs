using System.Collections;
using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Commands;

public class PrintEnvCommand : ICommand
{
    public IEnumerable<string> Names { get; } = new[] { "printenv" };

    public string Description => "print all or part of environment.";

    public ValueTask InvokeAsync(IConsoleHost consoleHost, string[] args, CancellationToken cancellationToken)
    {
        var envVals = Environment.GetEnvironmentVariables();

        if (args.Length == 1)
        {
            foreach (DictionaryEntry envVal in envVals)
            {
                consoleHost.WriteLine($"{envVal.Key}={envVal.Value}");
            }
        }
        else
        {
            foreach (var arg in args.Skip(1))
            {
                if (!envVals.Contains(arg)) continue;
                consoleHost.WriteLine(envVals[arg]?.ToString() ?? "");
            }
        }
        return ValueTask.CompletedTask;
    }
}
