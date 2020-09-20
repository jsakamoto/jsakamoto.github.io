using System.Collections.Generic;
using System.Linq;
using CUIFlavoredPortfolioSite.Services.CommandSet;

namespace CUIFlavoredPortfolioSite.Services
{
    public class CommandCompletion
    {
        private readonly IEnumerable<string> _CommandNames;

        private string _LastCompleted = "";

        private string[] _LastCandidates = null;

        private int _LastCandidateIndex = -1;

        public CommandCompletion(IEnumerable<ICommand> commands)
        {
            _CommandNames = commands
                .SelectMany(cmd => cmd.Names)
                .OrderBy(name => name)
                .ToArray();
        }

        public string Completion(string commandText)
        {
            if (_LastCandidateIndex >= 0 && _LastCandidates[_LastCandidateIndex] == commandText)
            {
                _LastCandidateIndex = (_LastCandidateIndex + 1) % _LastCandidates.Length;
                return _LastCandidates[_LastCandidateIndex];
            }

            _LastCandidates = _CommandNames
                .Where(name => name.StartsWith(commandText))
                .ToArray();
            _LastCandidateIndex = _LastCandidates.Length > 0 ? 0 : -1;

            if (_LastCandidateIndex == 0)
            {
                return _LastCandidates[0];
            }

            return commandText;
        }
    }
}
