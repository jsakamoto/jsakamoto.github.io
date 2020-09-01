using System.Collections.Generic;

namespace CUIFlavoredPortfolioSite.Services.ConsoleHost
{
    public interface IConsoleHost
    {
        IEnumerable<ConsoleLine> Lines { get; }
        IConsoleHost Write(string text);
        IConsoleHost WriteLine();
        IConsoleHost WriteLine(string text);
    }
}
