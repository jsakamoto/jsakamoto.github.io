using System;
using System.Collections.Generic;

namespace CUIFlavoredPortfolioSite.Services
{
    public class CommandHistory
    {
        private List<string> History { get; } = new List<string>();

        private int HistoryIndex = 0;

        public int HistoryBuffSize { get; set; } = 100;

        private bool FirstHistoryBack = true;

        public void Push(string commandText)
        {
            FirstHistoryBack = true;
            if (string.IsNullOrWhiteSpace(commandText)) return;

            if (History.Count == 0 || (History.Count > HistoryIndex && History[HistoryIndex] != commandText))
            {
                History.Insert(0, commandText);
                if (History.Count > HistoryBuffSize) History.RemoveAt(History.Count - 1);
                HistoryIndex = 0;
            }

        }

        public bool TryGetPrevious(out string commandText)
        {
            commandText = "";

            if (!FirstHistoryBack) HistoryIndex++;

            if (HistoryIndex > History.Count - 1)
            {
                HistoryIndex = Math.Max(0, History.Count - 1);
                return false;
            }

            commandText = History[HistoryIndex];
            FirstHistoryBack = false;
            return true;
        }

        public bool TryGetNext(out string commandText)
        {
            commandText = "";

            if (HistoryIndex == 0) { return false; }

            HistoryIndex--;
            if (HistoryIndex < 0 || HistoryIndex > History.Count - 1)
            {
                HistoryIndex = -1;
                return false;
            }

            commandText = History[HistoryIndex];
            FirstHistoryBack = false;
            return true;
        }
    }
}
