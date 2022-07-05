namespace CUIFlavoredPortfolioSite.Services.ConsoleHost;

public class ConsoleFragment
{
    public int Id { get; }

    public string Text { get; set; }

    public string ForeColor { get; }

    public string Link { get; }

    public bool NoWrap { get; set; }

    public int Indent { get; set; }

    public ConsoleFragment(int id, string text, string foreColor = null, string link = null)
    {
        this.Id = id;
        this.Text = text;
        this.ForeColor = foreColor;
        this.Link = link;
    }
}
