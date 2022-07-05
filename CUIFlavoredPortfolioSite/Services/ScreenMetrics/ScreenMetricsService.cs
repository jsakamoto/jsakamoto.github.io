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
		return new ValueTask<Metrics>(new Metrics(9, 80));
		//return this._JSRuntime.InvokeAsync<Metrics>("getScreenMetrics");
	}
}
