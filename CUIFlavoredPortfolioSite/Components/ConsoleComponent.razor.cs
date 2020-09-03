using System;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Components
{
    public partial class ConsoleComponent : IDisposable
    {
        private string ForeColorOf(ConsoleFragment fragment) => fragment.ForeColor;

        protected override void OnInitialized()
        {
            ConsoleHost.StateHasChanged += ConsoleHost_StateHasChanged;
        }

        private void ConsoleHost_StateHasChanged(object sender, EventArgs e)
        {
            StateHasChanged();
        }

        public void Dispose()
        {
            ConsoleHost.StateHasChanged -= ConsoleHost_StateHasChanged;
        }
    }
}
