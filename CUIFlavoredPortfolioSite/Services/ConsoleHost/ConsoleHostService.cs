using System.Text.RegularExpressions;

namespace CUIFlavoredPortfolioSite.Services.ConsoleHost;

public class ConsoleHostService : IConsoleHost
{
    private readonly List<ConsoleLine> _Lines = new List<ConsoleLine>();

    public IEnumerable<ConsoleLine> Lines => this._Lines;

    private ConsoleLine _CurrentLine = null;

    private string _CurrentForeColor = "#cccccc";

    private int _IdSequence = 0;

    public event EventHandler StateChanged;

    public void StateHasChanged()
    {
        StateChanged?.Invoke(this, EventArgs.Empty);
    }

    public IConsoleHost Write(string text)
    {
        if (this._CurrentLine == null)
        {
            this._CurrentLine = this.NewLine();
        }
        var lines = text.Split('\n').Select(t => t.TrimEnd('\r')).ToArray();
        for (var l = 0; l < lines.Length; l++)
        {
            if (l > 0) this._CurrentLine = this.NewLine();
            this._CurrentLine.AddFragments(this.CreateFragments(lines[l]));
        }

        this.StateHasChanged();
        return this;
    }

    public IConsoleHost WriteLine() => this.WriteLine("");

    public IConsoleHost WriteLine(string text)
    {
        var targetLine = this._CurrentLine ?? this.NewLine();

        var lines = text.Split('\n').Select(t => t.TrimEnd('\r')).ToArray();
        for (var l = 0; l < lines.Length; l++)
        {
            if (l > 0) targetLine = this.NewLine();
            targetLine.AddFragments(this.CreateFragments(lines[l]));
        }

        this._CurrentLine = null;
        this.StateHasChanged();
        return this;
    }

    public ConsoleLine NewLine()
    {
        var line = new ConsoleLine(this._IdSequence++);
        this._Lines.Add(line);
        return line;
    }

    public void RemoveLines(int lastNLine)
    {
        var index = Math.Max(0, this._Lines.Count - lastNLine);
        var count = Math.Min(this._Lines.Count, lastNLine);
        this._Lines.RemoveRange(index, count);
    }

    public void Clear()
    {
        this._Lines.Clear();
        this.StateHasChanged();
    }

    private IEnumerable<ConsoleFragment> CreateFragments(string text)
    {
        var ansiColorPatterns = Regex.Matches(text, "\x1b\\[\\d+m")
            .Select(m => (m.Success, m.Value, m.Index, m.Length))
            .ToList();

        if (ansiColorPatterns.Count == 0 || ansiColorPatterns[0].Index > 0)
            ansiColorPatterns.Insert(0, (false, "", 0, 0));

        var lastPattern = ansiColorPatterns[ansiColorPatterns.Count - 1];
        if (ansiColorPatterns.Count == 1 || lastPattern.Index + lastPattern.Length < text.Length)
            ansiColorPatterns.Add((false, "", text.Length, 0));

        for (var i = 0; i < ansiColorPatterns.Count - 1; i++)
        {
            var headPattern = ansiColorPatterns[i];
            var tailPattern = ansiColorPatterns[i + 1];
            this.UpdateCurrentForeColor(headPattern);

            var fragmentIndex = headPattern.Index + headPattern.Length;
            var fragmentLength = tailPattern.Index - fragmentIndex;
            if (i == 0 || fragmentLength > 0)
            {
                var textFragment = text.Substring(fragmentIndex, fragmentLength);

                foreach (var fragment in this.CreateHyperLinkedFragments(textFragment))
                {
                    yield return fragment;
                }
            }
            this.UpdateCurrentForeColor(tailPattern);
        }
    }

    private IEnumerable<ConsoleFragment> CreateHyperLinkedFragments(string text)
    {
        var linkPatterns = Regex.Matches(text, @"\[(?<text>[^\]]+?)\]\((?<link>[^)]+?)\)")
            .Select(m => (Text: m.Groups["text"].Value, Link: m.Groups["link"].Value, m.Index, m.Length))
            .ToList();

        if (!linkPatterns.Any())
        {
            yield return new ConsoleFragment(this._IdSequence++, text, this._CurrentForeColor, link: null);
            yield break;
        }

        var linkPatPos = 0;
        var textPos = 0;
        while (textPos < text.Length)
        {
            var pattern = linkPatPos < linkPatterns.Count ? linkPatterns[linkPatPos++] : ("", null, text.Length, 0);
            var textLen = pattern.Index - textPos;
            if (textLen > 0) yield return new ConsoleFragment(this._IdSequence++, text.Substring(textPos, textLen), this._CurrentForeColor, null);

            textPos += textLen;

            if (pattern.Length > 0)
                yield return new ConsoleFragment(this._IdSequence++, pattern.Text, this._CurrentForeColor, pattern.Link);

            textPos += pattern.Length;
        }
    }

    private void UpdateCurrentForeColor((bool Success, string Value, int Index, int Length) ansiColorPattern)
    {
        if (ansiColorPattern.Success)
            this._CurrentForeColor = ANSIColorToRGB.TryGetRGB(ansiColorPattern.Value, out var rgb) ? rgb : this._CurrentForeColor;
    }
}
