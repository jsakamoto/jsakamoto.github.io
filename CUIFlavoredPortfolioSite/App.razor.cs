using System;
using System.Linq;
using System.Threading.Tasks;
using Figgle;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using static Toolbelt.AnsiEscCode.Colorize;

namespace CUIFlavoredPortfolioSite
{
    public partial class App
    {
        private ElementReference CommandLineInput;

        private string CommandLineInputText { get; set; } = "";

        protected override void OnInitialized()
        {
            ConsoleHost.WriteLine(Yellow(FiggleFonts.Slant.Render("I'm")));
            ConsoleHost.WriteLine(Yellow(FiggleFonts.Slant.Render("J.Sakamoto !")));
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
                ConsoleHost.WriteLine(Green("jsakamoto") + "$ " + CommandLineInputText);
                ProcessCommandLine();
                CommandLineInputText = "";
            }
        }

        private void ProcessCommandLine()
        {
            if (CommandLineInputText != "")
            {
                var commandArgs = CommandLineInputText.Split(' ');
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
                    ConsoleHost.WriteLine(commandName + ": command not found");
                }
            }
        }
    }
}
