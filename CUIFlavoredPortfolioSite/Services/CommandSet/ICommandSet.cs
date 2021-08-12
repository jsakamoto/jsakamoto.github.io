namespace CUIFlavoredPortfolioSite.Services.CommandSet;

public interface ICommandSet
{
    bool TryGetCommand(string commandName, out ICommand command);
}
