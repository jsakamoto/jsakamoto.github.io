namespace CUIFlavoredPortfolioSite.Services.ScreenMetrics;

public class Metrics
{
    public int CharWidthPx { get; }

    public int ScreenWidthChar { get; }

    public Metrics(int charWidthPx, int screenWidthChar)
    {
        this.CharWidthPx = charWidthPx;
        this.ScreenWidthChar = screenWidthChar;
    }
}
