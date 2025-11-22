using CUIFlavoredPortfolioSite.Services.ConsoleHost;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CUIFlavoredPortfolioSite.Components;

public partial class ConsoleComponent : IDisposable
{
    private ElementReference _ConsoleComponentElement;

    private string? ForeColorOf(ConsoleFragment fragment) => fragment.ForeColor;

    private double MarginLeftOf(ConsoleFragment fragment) => fragment.Indent * this._CharWidthPx;

    private double _CharWidthPx = 0;

    private int _LastConsoleLinesCount = 0;

    protected override void OnInitialized()
    {
        this.ConsoleHost.StateChanged += this.ConsoleHost_StateHasChanged;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var metrics = await this.ScreenMetrics.GetMetricsAsync();
            this._CharWidthPx = metrics.CharWidthPx;
        }
    }

    private void ConsoleHost_StateHasChanged(object? sender, EventArgs e)
    {
        this.StateHasChanged();
        if (this._LastConsoleLinesCount != this.ConsoleHost.Lines.Count())
        {
            this._LastConsoleLinesCount = this.ConsoleHost.Lines.Count();
            var jsInProcRuntime = this.JSRuntime as IJSInProcessRuntime;
            if (jsInProcRuntime != null)
            {
                jsInProcRuntime.InvokeVoid("Helper.scrollIntoView", this._ConsoleComponentElement, false);
            }
        }
    }

    public void Dispose()
    {
        this.ConsoleHost.StateChanged -= this.ConsoleHost_StateHasChanged;
    }
}
