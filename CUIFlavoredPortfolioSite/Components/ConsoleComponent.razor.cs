using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Components;

public partial class ConsoleComponent : IDisposable
{
    private string ForeColorOf(ConsoleFragment fragment) => fragment.ForeColor;

    private double MarginLeftOf(ConsoleFragment fragment) => fragment.Indent * this._CharWidthPx;

    private double _CharWidthPx = 0;

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

    private void ConsoleHost_StateHasChanged(object sender, EventArgs e)
    {
        this.StateHasChanged();
    }

    public void Dispose()
    {
        this.ConsoleHost.StateChanged -= this.ConsoleHost_StateHasChanged;
    }
}
