namespace CUIFlavoredPortfolioSite.Services.ConsoleHost;

public interface IConsoleHost
{
    event EventHandler StateChanged;
    IEnumerable<ConsoleLine> Lines { get; }
    IConsoleHost Write(string text);
    IConsoleHost WriteLine();
    IConsoleHost WriteLine(string text);
    ConsoleLine NewLine();
    void RemoveLines(int lastNline);
    void Clear();
    void StateHasChanged();
}
