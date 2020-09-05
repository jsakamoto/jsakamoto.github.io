using System.Collections.Generic;
using System.Linq;

namespace CUIFlavoredPortfolioSite.Services.CommandSet
{
    public class CommandSetService : ICommandSet
    {
        private readonly IEnumerable<ICommand> _Commands;

        public CommandSetService(IEnumerable<ICommand> commands)
        {
            _Commands = commands;
        }

        public bool TryGetCommand(string commandName, out ICommand command)
        {
            command = _Commands.FirstOrDefault(cmd => cmd.Names.Contains(commandName));
            return command != null;
        }
    }
}
