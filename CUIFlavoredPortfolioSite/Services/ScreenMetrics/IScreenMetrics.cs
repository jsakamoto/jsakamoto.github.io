namespace CUIFlavoredPortfolioSite.Services.ScreenMetrics;
public interface IScreenMetrics
{
    ValueTask<Metrics> GetMetricsAsync();
}
