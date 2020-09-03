using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CUIFlavoredPortfolioSite.Services.ConsoleHost
{
    public class ConsoleHostService : IConsoleHost
    {
        private readonly List<ConsoleLine> _Lines = new List<ConsoleLine>();

        public IEnumerable<ConsoleLine> Lines => _Lines;

        private ConsoleLine _CurrentLine = null;

        private string _CurrentForeColor = "#cccccc";

        private int _IdSequence = 0;

        public IConsoleHost Write(string text)
        {
            if (_CurrentLine == null)
            {
                _CurrentLine = NewLine();
            }
            _CurrentLine.AddFragments(CreateFragments(text));
            return this;
        }

        public IConsoleHost WriteLine() => WriteLine("");

        public IConsoleHost WriteLine(string text)
        {
            var targetLine = _CurrentLine ?? NewLine();
            targetLine.AddFragments(CreateFragments(text));
            _CurrentLine = null;
            return this;
        }

        private ConsoleLine NewLine()
        {
            var line = new ConsoleLine(_IdSequence++);
            _Lines.Add(line);
            return line;
        }

        public void Clear()
        {
            _Lines.Clear();
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
                UpdateCurrentForeColor(headPattern);

                var fragmentIndex = headPattern.Index + headPattern.Length;
                var fragmentLength = tailPattern.Index - fragmentIndex;
                if (i == 0 || fragmentLength > 0)
                {
                    var textFragment = text.Substring(fragmentIndex, fragmentLength);
                    yield return new ConsoleFragment(_IdSequence++, textFragment, _CurrentForeColor);
                }
                UpdateCurrentForeColor(tailPattern);
            }
        }

        private void UpdateCurrentForeColor((bool Success, string Value, int Index, int Length) ansiColorPattern)
        {
            if (ansiColorPattern.Success)
                _CurrentForeColor = ANSIColorToRGB.TryGetRGB(ansiColorPattern.Value, out var rgb) ? rgb : _CurrentForeColor;
        }
    }
}
