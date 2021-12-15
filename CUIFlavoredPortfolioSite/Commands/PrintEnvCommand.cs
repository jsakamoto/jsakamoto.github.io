using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;
using System.Collections;

namespace CUIFlavoredPortfolioSite.Commands;

public class PrintEnvCommand : ICommand
{
    public IEnumerable<string> Names { get; } = new[] { "printenv" };

    public string Description => "print all or part of environment.";

    public void Invoke(IConsoleHost consoleHost, string[] args)
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
                consoleHost.WriteLine(envVals[arg].ToString());
            }
        }
    }
}
