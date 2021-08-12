namespace CUIFlavoredPortfolioSite.Services;

public class CommandHistory
{
    private List<string> History { get; } = new List<string>();

    private int HistoryIndex = 0;

    public int HistoryBuffSize { get; set; } = 100;

    private bool FirstHistoryBack = true;

    public void Push(string commandText)
    {
        this.FirstHistoryBack = true;
        if (string.IsNullOrWhiteSpace(commandText)) return;

        if (this.History.Count == 0 || (this.History.Count > this.HistoryIndex && this.History[this.HistoryIndex] != commandText))
        {
            this.History.Insert(0, commandText);
            if (this.History.Count > this.HistoryBuffSize) this.History.RemoveAt(this.History.Count - 1);
            this.HistoryIndex = 0;
        }

    }

    public bool TryGetPrevious(out string commandText)
    {
        commandText = "";

        if (!this.FirstHistoryBack) this.HistoryIndex++;

        if (this.HistoryIndex > this.History.Count - 1)
        {
            this.HistoryIndex = Math.Max(0, this.History.Count - 1);
            return false;
        }

        commandText = this.History[this.HistoryIndex];
        this.FirstHistoryBack = false;
        return true;
    }

    public bool TryGetNext(out string commandText)
    {
        commandText = "";

        if (this.HistoryIndex == 0) { return false; }

        this.HistoryIndex--;
        if (this.HistoryIndex < 0 || this.HistoryIndex > this.History.Count - 1)
        {
            this.HistoryIndex = -1;
            return false;
        }

        commandText = this.History[this.HistoryIndex];
        this.FirstHistoryBack = false;
        return true;
    }
}
