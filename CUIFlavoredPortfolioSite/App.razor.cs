using System;
using System.Linq;
using System.Threading.Tasks;
using CUIFlavoredPortfolioSite.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using static Toolbelt.AnsiEscCode.Colorize;

namespace CUIFlavoredPortfolioSite
{
    public partial class App
    {
        private ElementReference CommandLineInput;

        private string CommandLineInputText { get; set; } = "";

        private CommandHistory CommandHistory { get; } = new CommandHistory();

        private bool _Initialized = false;

        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(400);
#if RELEASE
            await TypeAndExecuteCommand("banner");

            await Task.Delay(400);
            await TypeAndExecuteCommand("profile");
#endif
            _Initialized = true;
            StateHasChanged();
        }

        private async Task TypeAndExecuteCommand(string text)
        {
            var r = new Random((int)(DateTime.Now.Ticks % int.MaxValue));
            foreach (var c in text)
            {
                CommandLineInputText += c;
                StateHasChanged();
                await Task.Delay(r.Next(50, 200));
            }
            await Task.Delay(400);
            ExecuteCommand();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await CommandLineInput.FocusAsync();
            await JS.InvokeVoidAsync("Helper.scrollIntoView", this.CommandLineInput);
        }

        private void OnKeyDownCommandLineInput(KeyboardEventArgs e)
        {
            if (!_Initialized) return;

            switch (e.Key)
            {
                case "Enter":
                    ExecuteCommand();
                    break;
                case "ArrowUp":
                    RecallHistory(CommandHistory.TryGetPrevious(out var prevCommand), prevCommand);
                    break;
                case "ArrowDown":
                    RecallHistory(CommandHistory.TryGetNext(out var nextCommand), nextCommand);
                    break;
                case "Tab":
                    CommandLineInputText = CommandCompletion.Completion(CommandLineInputText);
                    StateHasChanged();
                    break;
                default: break;
            }
        }

        private void RecallHistory(bool found, string commandText)
        {
            if (!found) return;
            CommandLineInputText = commandText;
            StateHasChanged();
        }

        private void ExecuteCommand()
        {
            ConsoleHost.WriteLine(Green("jsakamoto") + "$ " + CommandLineInputText);
            ProcessCommandLine(CommandLineInputText);
            CommandLineInputText = "";
            StateHasChanged();
        }

        private void ProcessCommandLine(string commandLineInputText)
        {
            CommandHistory.Push(commandLineInputText);

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

                ConsoleHost.WriteLine();
            }
        }
    }
}
