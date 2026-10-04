using TaskbarWidgets.Loader.Core;

namespace TaskbarWidgets.Loader;

public sealed class SleepForecastProvider : IWidgetProvider
{
    public string Id => "sleep-forecast";
    public Task RunAsync(WidgetProviderContext context, CancellationToken cancellationToken) =>
        SleepForecastWorker.RunAsync(cancellationToken);
}
