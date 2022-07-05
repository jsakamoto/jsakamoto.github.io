using Microsoft.JSInterop;

namespace CUIFlavoredPortfolioSite.Services.ScreenMetrics;

public class ScreenMetricsService : IScreenMetrics
{
	private readonly IJSRuntime _JSRuntime;

	public ScreenMetricsService(IJSRuntime jsRuntime)
	{
		this._JSRuntime = jsRuntime;
	}

	public ValueTask<Metrics> GetMetricsAsync()
	{
		return this._JSRuntime.InvokeAsync<Metrics>("Helper.getScreenMetrics");
	}
}
