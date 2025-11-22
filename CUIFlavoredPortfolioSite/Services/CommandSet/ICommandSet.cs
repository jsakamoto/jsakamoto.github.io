using System.Diagnostics.CodeAnalysis;

namespace CUIFlavoredPortfolioSite.Services.CommandSet;

public interface ICommandSet
{
    bool TryGetCommand(string commandName, [NotNullWhen(true)] out ICommand? command);
}
