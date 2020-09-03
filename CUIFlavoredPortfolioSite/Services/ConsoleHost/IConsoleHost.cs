using System;
using System.Collections.Generic;

namespace CUIFlavoredPortfolioSite.Services.ConsoleHost
{
    public interface IConsoleHost
    {
        event EventHandler StateHasChanged;
        IEnumerable<ConsoleLine> Lines { get; }
        IConsoleHost Write(string text);
        IConsoleHost WriteLine();
        IConsoleHost WriteLine(string text);
        void Clear();
    }
}
