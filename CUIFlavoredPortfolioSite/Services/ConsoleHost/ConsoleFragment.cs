namespace CUIFlavoredPortfolioSite.Services.ConsoleHost
{
    public class ConsoleFragment
    {
        public int Id { get; }

        public string Text { get; }

        public string ForeColor { get; }

        public ConsoleFragment(int id, string text, string foreColor)
        {
            Id = id;
            Text = text;
            ForeColor = foreColor;
        }
    }
}
