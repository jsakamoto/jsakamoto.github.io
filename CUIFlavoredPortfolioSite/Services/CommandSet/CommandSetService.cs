using System.Diagnostics.CodeAnalysis;

namespace CUIFlavoredPortfolioSite.Services.CommandSet;

public class CommandSetService : ICommandSet
{
    private readonly IEnumerable<ICommand> _Commands;

    public CommandSetService(IEnumerable<ICommand> commands)
    {
        this._Commands = commands;
    }

    public bool TryGetCommand(string commandName, [NotNullWhen(true)] out ICommand? command)
    {
        command = this._Commands.FirstOrDefault(cmd => cmd.Names.Contains(commandName));
        return command != null;
    }
}
