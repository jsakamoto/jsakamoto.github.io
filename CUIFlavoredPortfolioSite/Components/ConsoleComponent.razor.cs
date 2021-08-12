using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Components;

public partial class ConsoleComponent : IDisposable
{
    private string ForeColorOf(ConsoleFragment fragment) => fragment.ForeColor;

    protected override void OnInitialized()
    {
        this.ConsoleHost.StateHasChanged += this.ConsoleHost_StateHasChanged;
    }

    private void ConsoleHost_StateHasChanged(object sender, EventArgs e)
    {
        this.StateHasChanged();
    }

    public void Dispose()
    {
        this.ConsoleHost.StateHasChanged -= this.ConsoleHost_StateHasChanged;
    }
}
