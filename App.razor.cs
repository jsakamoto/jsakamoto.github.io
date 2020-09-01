using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CUIFlavoredPortfolioSite
{
    public partial class App
    {
        private ElementReference CommandLineInput;

        private string CommandLineInputText { get; set; } = "";

        protected override void OnInitialized()
        {
            ConsoleHost.Write("Hi,").Write("Evey").WriteLine("one.");
            ConsoleHost
                .WriteLine("Nice to meet you.")
                .WriteLine();

            ConsoleHost
                .WriteLine("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam nulla velit, finibus in erat et, dapibus aliquet dolor. Nullam molestie mollis dui, nec euismod mauris sagittis non. Praesent luctus augue quis sem tempus venenatis. Praesent vitae nisl nunc. Mauris vel pulvinar nunc, vel dignissim sem. Vivamus gravida risus odio, id auctor enim egestas ut. Nullam tellus nulla, finibus non blandit eu, pulvinar vitae tortor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed in est quis nibh dictum tristique.")
                .WriteLine("Morbi vel tellus quis elit accumsan pulvinar efficitur eu nisi. Pellentesque nec odio a eros dictum porttitor sit amet eu orci. Sed sed magna erat. In quis ligula quis diam viverra feugiat. Suspendisse in leo a nibh aliquet varius vel vel arcu. Aliquam pretium sapien id pellentesque rhoncus. Phasellus finibus mauris efficitur sem bibendum, vel tincidunt metus tincidunt. Phasellus consequat, dui vitae pellentesque efficitur, nunc turpis blandit lacus, sit amet feugiat urna felis sed ante. Morbi in finibus nisi.");
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
                CommandLineInputText = "";
            }
        }
    }
}
