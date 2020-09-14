namespace CUIFlavoredPortfolioSite.Services.ConsoleHost
{
    public class ConsoleFragment
    {
        public int Id { get; }

        public string Text { get; }

        public string ForeColor { get; }

        public string Link { get; }

        public ConsoleFragment(int id, string text, string foreColor, string link)
        {
            Id = id;
            Text = text;
            ForeColor = foreColor;
            Link = link;
        }
    }
}
