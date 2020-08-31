using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CUIFlavoredPortfolioSite
{
    public partial class App
    {
        private ElementReference CommandLineInput;

        private string CommandLineInputText { get; set; } = "";

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
                CommandLineInputText = "";
            }
        }
    }
}
