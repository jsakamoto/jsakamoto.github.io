using CUIFlavoredPortfolioSite.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using static Toolbelt.AnsiEscCode.Colorize;

namespace CUIFlavoredPortfolioSite;

public partial class App
{
    public enum RuntimeModes
    {
        Release,
        Debug
    }

    [Parameter]
    public RuntimeModes RuntimeMode { get; set; }
#if RELEASE
            = RuntimeModes.Release;
#else
            = RuntimeModes.Debug;
#endif

    private ElementReference CommandLineInput;

    private string CommandLineInputText { get; set; } = "";

    private CommandHistory CommandHistory { get; } = new CommandHistory();

    private bool _Initialized = false;

    protected override async Task OnInitializedAsync()
    {
        var cancellationToken = CancellationToken.None;
        await Task.Delay(400);
        if (this.RuntimeMode != RuntimeModes.Debug)
        {
            var todaysMMDD = DateTime.Now.Month * 100 + DateTime.Now.Day;
            if (todaysMMDD is >= 101 and <= 115)
            {
                await this.TypeAndExecuteCommandAsync("new-year-greeting", cancellationToken);
            }
            else
            {
                await this.TypeAndExecuteCommandAsync("banner", cancellationToken);
            }

            await Task.Delay(400);
            await this.TypeAndExecuteCommandAsync("profile", cancellationToken);
        }

        this._Initialized = true;
        this.StateHasChanged();
    }

    private async Task TypeAndExecuteCommandAsync(string text, CancellationToken cancellationToken)
    {
        var r = new Random((int)(DateTime.Now.Ticks % int.MaxValue));
        foreach (var c in text)
        {
            this.CommandLineInputText += c;
            this.StateHasChanged();
            await Task.Delay(r.Next(50, 200));
        }
        await Task.Delay(400);

        await this.ExecuteCommandAsync(cancellationToken);
    }

    private string GetTwitterShareButtonUrl()
    {
        const string description = "This is a CUI flavored portfolio site about @jsakamoto that he created using Blazor WebAssembly!";
        const string url = "https://jsakamoto.github.io/";
        return "https://twitter.com/intent/tweet" +
            $"?text={Uri.EscapeDataString(description)}" +
            $"&hashtags=Blazor" +
            $"&url={Uri.EscapeDataString(url)}";
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await this.CommandLineInput.FocusAsync();
        await this.JS.InvokeVoidAsync("Helper.scrollIntoView", this.CommandLineInput);
    }

    private async Task OnKeyDownCommandLineInput(KeyboardEventArgs e)
    {
        if (!this._Initialized) return;
        var cancelationToken = CancellationToken.None;

        switch (e.Code)
        {
            case "Enter":
                await this.ExecuteCommandAsync(cancelationToken);
                break;
            case "KeyL":
                if (e.CtrlKey) await this.ProcessCommandLineAsync("clear", cancelationToken, noSaveHistory: true);
                break;
            case "ArrowUp":
                this.RecallHistory(this.CommandHistory.TryGetPrevious(out var prevCommand), prevCommand);
                break;
            case "ArrowDown":
                this.RecallHistory(this.CommandHistory.TryGetNext(out var nextCommand), nextCommand);
                break;
            case "Tab":
                this.CommandLineInputText = this.CommandCompletion.Completion(this.CommandLineInputText);
                this.StateHasChanged();
                break;
            default: break;
        }
    }

    private void RecallHistory(bool found, string commandText)
    {
        if (!found) return;
        this.CommandLineInputText = commandText;
        this.StateHasChanged();
    }

    private async ValueTask ExecuteCommandAsync(CancellationToken cancellationToken)
    {
        this.ConsoleHost.WriteLine($"{Green("jsakamoto")}:{Blue(Environment.CurrentDirectory)}$ {this.CommandLineInputText}");
        await this.ProcessCommandLineAsync(this.CommandLineInputText, cancellationToken);
        this.CommandLineInputText = "";
        this.StateHasChanged();
    }

    private async ValueTask ProcessCommandLineAsync(string commandLineInputText, CancellationToken cancellationToken, bool noSaveHistory = false)
    {
        if (!noSaveHistory) this.CommandHistory.Push(commandLineInputText);

        if (commandLineInputText != "")
        {
            var commandArgs = commandLineInputText.Split(' ');
            var commandName = commandArgs.First();
            if (this.CommandSet.TryGetCommand(commandName, out var command))
            {
                try
                {
                    await command.InvokeAsync(this.ConsoleHost, commandArgs, cancellationToken);
                }
                catch (Exception e)
                {
                    this.ConsoleHost.WriteLine(Red(e.ToString()));
                }
            }
            else
            {
                this.ConsoleHost.WriteLine($"{commandName}: command not found. Please try out {Cyan("help")} command.");
            }

            this.ConsoleHost.WriteLine();
        }
    }
}
