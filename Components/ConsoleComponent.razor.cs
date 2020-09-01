using System.Drawing;
using CUIFlavoredPortfolioSite.Services.ConsoleHost;

namespace CUIFlavoredPortfolioSite.Components
{
    public partial class ConsoleComponent
    {
        private string ForeColorOf(ConsoleFragment fragment) => ToCssColorText(fragment.ForeColor);

        private string ToCssColorText(Color color) => $"#{color.R:x2}{color.G:x2}{color.B:x2}";
    }
}
