using CUIFlavoredPortfolioSite.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Toolbelt.Blazor.HotKeys2;
using static Toolbelt.AnsiEscCode.Colorize;

namespace CUIFlavoredPortfolioSite;

public partial class App : IAsyncDisposable
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

    private bool CommandProcessing = false;

    private bool _Initialized = false;

    private HotKeysContext _HotKeysContext;

    protected override async Task OnInitializedAsync()
    {
        this._HotKeysContext = this.HotKeys.CreateContext().Add(ModCode.Ctrl, Code.C, () => this.OnCtrlC());

        await Task.Delay(400);
        if (this.RuntimeMode != RuntimeModes.Debug)
        {
            var todaysMMDD = DateTime.Now.Month * 100 + DateTime.Now.Day;
            if (todaysMMDD is >= 101 and <= 115)
            {
                await this.TypeAndExecuteCommandAsync("new-year-greeting");
            }
            else
            {
                await this.TypeAndExecuteCommandAsync("banner");
            }

            await Task.Delay(400);
            await this.TypeAndExecuteCommandAsync("profile");
        }

        this._Initialized = true;
        this.StateHasChanged();
    }

    private async Task TypeAndExecuteCommandAsync(string text)
    {
        var r = new Random((int)(DateTime.Now.Ticks % int.MaxValue));
        foreach (var c in text)
        {
            this.CommandLineInputText += c;
            this.StateHasChanged();
            await Task.Delay(r.Next(50, 200));
        }
        await Task.Delay(400);

        await this.ExecuteCommandAsync();
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

    private void OnCtrlC()
    {
        this._CommandCanceller?.Cancel();
    }

    private async Task OnKeyDownCommandLineInput(KeyboardEventArgs e)
    {
        if (!this._Initialized) return;

        switch (e.Code)
        {
            case "Enter":
                await this.ExecuteCommandAsync();
                break;
            case "KeyL":
                if (e.CtrlKey) await this.ProcessCommandLineAsync("clear", noSaveHistory: true);
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

    private async ValueTask ExecuteCommandAsync()
    {
        this.ConsoleHost.WriteLine($"{Green("jsakamoto")}:{Blue(Environment.CurrentDirectory)}$ {this.CommandLineInputText}");
        await this.ProcessCommandLineAsync(this.CommandLineInputText);
        this.CommandLineInputText = "";
        this.StateHasChanged();
    }

    private CancellationTokenSource _CommandCanceller;

    private async ValueTask ProcessCommandLineAsync(string commandLineInputText, bool noSaveHistory = false)
    {
        this._CommandCanceller?.Dispose();
        this._CommandCanceller = new CancellationTokenSource();

        if (!noSaveHistory) this.CommandHistory.Push(commandLineInputText);

        if (commandLineInputText != "")
        {
            var commandArgs = commandLineInputText.Split(' ');
            var commandName = commandArgs.First();
            if (this.CommandSet.TryGetCommand(commandName, out var command))
            {
                try
                {
                    this.CommandProcessing = true;
                    await command.InvokeAsync(this.ConsoleHost, commandArgs, this._CommandCanceller.Token);
                }
                catch (Exception e)
                {
                    this.ConsoleHost.WriteLine(Red(e.ToString()));
                }
                finally
                {
                    this.CommandProcessing = false;
                    if (this._CommandCanceller?.IsCancellationRequested == true)
                    {
                        this.ConsoleHost.WriteLine("^C");
                    }
                }
            }
            else
            {
                this.ConsoleHost.WriteLine($"{commandName}: command not found. Please try out {Cyan("help")} command.");
            }

            this.ConsoleHost.WriteLine();
        }
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        if (this._HotKeysContext is not null) await this._HotKeysContext.DisposeAsync();
    }
}
