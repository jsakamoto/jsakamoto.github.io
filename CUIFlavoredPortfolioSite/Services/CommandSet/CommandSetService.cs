using System.Collections.Generic;
using System.Linq;
using CUIFlavoredPortfolioSite.Services.CommandSet.Commands;

namespace CUIFlavoredPortfolioSite.Services.CommandSet
{
    public class CommandSetService : ICommandSet
    {
        private readonly List<ICommand> _Commands = new List<ICommand>();

        public CommandSetService()
        {
            _Commands.AddRange(new ICommand[] {
                new ClearCommand()
            });
        }

        public bool TryGetCommand(string commandName, out ICommand command)
        {
            command = _Commands.FirstOrDefault(cmd => cmd.Names.Contains(commandName));
            return command != null;
        }
    }
}
