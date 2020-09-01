using System.Drawing;

namespace CUIFlavoredPortfolioSite.Services.ConsoleHost
{
    public class ConsoleFragment
    {
        public int Id { get; }

        public string Text { get; }

        public Color ForeColor { get; }

        public ConsoleFragment(int id, string text, Color foreColor)
        {
            Id = id;
            Text = text;
            ForeColor = foreColor;
        }
    }
}
