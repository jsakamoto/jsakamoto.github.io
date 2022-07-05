using CUIFlavoredPortfolioSite.Services.CommandSet;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;
using CUIFlavoredPortfolioSite.Services.ScreenMetrics;

namespace CUIFlavoredPortfolioSite.Commands;

public class SlCommand : ICommand
{
    public IEnumerable<string> Names { get; } = new[] { "sl" };

    public string Description => "display animations aimed to correct users who accidentally enter sl instead of ls.";

    private const int HeightOfSmoke = 6;
    private const int HeightOfFunnel = 7;
    private const int HeightOfWheel = 3;
    private const int Height = HeightOfSmoke + HeightOfFunnel + HeightOfSmoke;

    private const int OffsetOfChimney = 7;

    private readonly List<string> _Funnel = new() {
        "      ====        ________                ___________ ",
        "  _D _|  |_______/        \\__I_I_____===__|_________| ",
        "   |(_)---  |   H\\________/ |   |        =|___ ___|   ",
        "   /     |  |   H  |  |     |   |         ||_| |_||   ",
        "  |      |  |   H  |__--------------------| [___] |   ",
        "  | ________|___H__/__|_____/[][]~\\_______|       |   ",
        "  |/ |   |-----------I_____I [][] []  D   |=======|__ ",
    };

    private readonly string[][] _Wheels = new[] {
        new[] {
            "__/ =| o |=-~~\\  /~~\\  /~~\\  /~~\\ ____Y___________|__ ",
            " |/-=|___|=    ||    ||    ||    |_____/~\\___/        ",
            "  \\_/      \\O=====O=====O=====O_/      \\_/            ",
        },
        new[] {
            "__/ =| o |=-~~\\  /~~\\  /~~\\  /~~\\ ____Y___________|__ ",
            " |/-=|___|=O=====O=====O=====O   |_____/~\\___/        ",
            "  \\_/      \\__/  \\__/  \\__/  \\__/      \\_/            ",
        },
        new[] {
            "__/ =| o |=-O=====O=====O=====O \\ ____Y___________|__ ",
            " |/-=|___|=    ||    ||    ||    |_____/~\\___/        ",
            "  \\_/      \\__/  \\__/  \\__/  \\__/      \\_/            ",
        },
        new[] {
            "__/ =| o |=-~O=====O=====O=====O\\ ____Y___________|__ ",
            " |/-=|___|=    ||    ||    ||    |_____/~\\___/        ",
            "  \\_/      \\__/  \\__/  \\__/  \\__/      \\_/            ",
        },
        new[] {
            "__/ =| o |=-~~\\  /~~\\  /~~\\  /~~\\ ____Y___________|__ ",
            " |/-=|___|=   O=====O=====O=====O|_____/~\\___/        ",
            "  \\_/      \\__/  \\__/  \\__/  \\__/      \\_/            ",
        },
        new[] {
            "__/ =| o |=-~~\\  /~~\\  /~~\\  /~~\\ ____Y___________|__ ",
            " |/-=|___|=    ||    ||    ||    |_____/~\\___/        ",
            "  \\_/      \\_O=====O=====O=====O/      \\_/            ",
        }
    };

    private readonly string[] _Coal = new[] {
        "                              ",
        "                              ",
        "    _________________         ",
        "   _|                \\_____A  ",
        " =|                        |  ",
        " -|                        |  ",
        "__|________________________|_ ",
        "|__________________________|_ ",
        "   |_D__D__D_|  |_D__D__D_|   ",
        "    \\_/   \\_/    \\_/   \\_/    ",
    };

    private readonly string[][] _Smokes = new[] {
        new[]{
            "              (  ) (@@) ( )  (@)  ()    @@    O     @     O     @      O",
            "         (@@@)                                                          ",
            "     (    )                                                             ",
            "  (@@@@)                                                                ",
            "                                                                        ",
            "(   )                                                                   ",
        },
        new [] {
            "              (@@) (  ) (@)  ( )  @@    ()    @     O     @     O      @",
            "         (   )                                                          ",
            "     (@@@@)                                                             ",
            "  (    )                                                                ",
            "                                                                        ",
            "(@@@)                                                                   ",
        },
    };

    private readonly IScreenMetrics _ScreenMetrics;

    public SlCommand(IScreenMetrics screenMetrics)
    {
        this._ScreenMetrics = screenMetrics;
    }

    public async ValueTask InvokeAsync(IConsoleHost consoleHost, string[] args, CancellationToken cancellationToken)
    {
        var metrics = await this._ScreenMetrics.GetMetricsAsync();
        var screenWidthChar = metrics.ScreenWidthChar;

        var rows = Enumerable.Range(0, Height).Select(n => new ConsoleFragment(n, "") { NoWrap = true, Indent = screenWidthChar }).ToArray();
        foreach (var row in rows) { consoleHost.NewLine().AddFragment(row); }

        var trainWidth = 0;
        for (var i = 0; i < HeightOfFunnel; i++)
        {
            var text = this._Funnel[i] + this._Coal[i];
            rows[HeightOfSmoke + i].Text = text;
            trainWidth = Math.Max(trainWidth, text.Length);
        }

        for (var clock = 0; clock < screenWidthChar + trainWidth + 5; clock++)
        {
            if (cancellationToken.IsCancellationRequested) break;

            var indent = screenWidthChar - clock;
            foreach (var row in rows) { row.Indent = indent; }

            var paddingLeftOfSmoke = "".PadLeft(OffsetOfChimney + (clock % 4), ' ');
            var smokePatternIndex = (clock / 4) % this._Smokes.Length;
            var smokePattern = this._Smokes[smokePatternIndex];
            for (var i = 0; i < smokePattern.Length; i++)
            {
                rows[i].Text = paddingLeftOfSmoke + smokePattern[i];
            }

            var wheelPatternIndex = clock % this._Wheels.Length;
            var wheelPattern = this._Wheels[this._Wheels.Length - 1 - wheelPatternIndex];
            for (var i = 0; i < wheelPattern.Length; i++)
            {
                rows[HeightOfFunnel + HeightOfSmoke + i].Text = wheelPattern[i] + this._Coal[HeightOfFunnel + i];
            }

            consoleHost.StateHasChanged();
            await Task.Delay(50);
        }

        consoleHost.RemoveLines(Height);

        await Task.CompletedTask;
    }
}
