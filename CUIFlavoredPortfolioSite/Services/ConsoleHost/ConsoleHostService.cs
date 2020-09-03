using System.Collections.Generic;
using System.Drawing;

namespace CUIFlavoredPortfolioSite.Services.ConsoleHost
{
    public class ConsoleHostService : IConsoleHost
    {
        private readonly List<ConsoleLine> _Lines = new List<ConsoleLine>();

        public IEnumerable<ConsoleLine> Lines => _Lines;

        private ConsoleLine _CurrentLine = null;

        private Color _CurrentForeColor = Color.FromArgb(0xcc, 0xcc, 0xcc);

        private int _IdSequence = 0;

        public IConsoleHost Write(string text)
        {
            if (_CurrentLine == null)
            {
                _CurrentLine = NewLine();
            }
            _CurrentLine.AddFragment(CreateFragment(text));
            return this;
        }

        public IConsoleHost WriteLine() => WriteLine("");

        public IConsoleHost WriteLine(string text)
        {
            var targetLine = _CurrentLine ?? NewLine();
            targetLine.AddFragment(CreateFragment(text));
            _CurrentLine = null;
            return this;
        }

        private ConsoleLine NewLine()
        {
            var line = new ConsoleLine(_IdSequence++);
            _Lines.Add(line);
            return line;
        }

        private ConsoleFragment CreateFragment(string text)
        {
            return new ConsoleFragment(_IdSequence++, text, _CurrentForeColor);
        }

        public void Clear()
        {
            _Lines.Clear();
        }
    }
}
