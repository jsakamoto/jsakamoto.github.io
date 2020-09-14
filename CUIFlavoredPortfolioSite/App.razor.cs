using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using static Toolbelt.AnsiEscCode.Colorize;

namespace CUIFlavoredPortfolioSite
{
    public partial class App
    {
        private ElementReference CommandLineInput;

        private string CommandLineInputText { get; set; } = "";

        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(500);
            ExecuteCommand("banner");
            await Task.Delay(500);
            ExecuteCommand("profile");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await CommandLineInput.FocusAsync();
        }

        private async Task OnBlurCommandLineInput()
        {
            await CommandLineInput.FocusAsync();
        }

        private void OnKeyUpCommandLineInput(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                ExecuteCommand(CommandLineInputText);
                CommandLineInputText = "";
            }
        }

        private void ExecuteCommand(string commandLineInputText)
        {
            ConsoleHost.WriteLine(Green("jsakamoto") + "$ " + commandLineInputText);
            ProcessCommandLine(commandLineInputText);
        }

        private void ProcessCommandLine(string commandLineInputText)
        {
            if (commandLineInputText != "")
            {
                var commandArgs = commandLineInputText.Split(' ');
                var commandName = commandArgs.First();
                if (CommandSet.TryGetCommand(commandName, out var command))
                {
                    try
                    {
                        command.Invoke(ConsoleHost, commandArgs);
                    }
                    catch (Exception e)
                    {
                        ConsoleHost.WriteLine(Red(e.ToString()));
                    }
                }
                else
                {
                    ConsoleHost.WriteLine($"{commandName}: command not found. Please try out {Cyan("help")} command.");
                }
            }
        }
    }
}
