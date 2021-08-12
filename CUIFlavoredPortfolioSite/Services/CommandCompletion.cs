using CUIFlavoredPortfolioSite.Services.CommandSet;

namespace CUIFlavoredPortfolioSite.Services;

public class CommandCompletion
{
    private readonly IEnumerable<string> _CommandNames;

    private string[] _LastCandidates = null;

    private int _LastCandidateIndex = -1;

    public CommandCompletion(IEnumerable<ICommand> commands)
    {
        this._CommandNames = commands
            .SelectMany(cmd => cmd.Names)
            .OrderBy(name => name)
            .ToArray();
    }

    public string Completion(string commandText)
    {
        if (this._LastCandidateIndex >= 0 && this._LastCandidates[this._LastCandidateIndex] == commandText)
        {
            this._LastCandidateIndex = (this._LastCandidateIndex + 1) % this._LastCandidates.Length;
            return this._LastCandidates[this._LastCandidateIndex];
        }

        this._LastCandidates = this._CommandNames
            .Where(name => name.StartsWith(commandText))
            .ToArray();
        this._LastCandidateIndex = this._LastCandidates.Length > 0 ? 0 : -1;

        if (this._LastCandidateIndex == 0)
        {
            return this._LastCandidates[0];
        }

        return commandText;
    }
}
