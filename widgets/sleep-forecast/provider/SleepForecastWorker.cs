using TaskbarWidgets.Loader.Core;

namespace TaskbarWidgets.Loader;

internal static class SleepForecastWorker
{
    private static readonly WidgetStateStore StateStore = new();

    // The slot is rendered from the local clock in Explorer, but its refresh is
    // driven by state-file changes. Publishing the current minute keeps the
    // displayed time correct while the XAML island is not compositing, when the
    // hook's animation tick is paused.
    public static async Task RunAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            if (ProviderActivation.IsEnabled("sleep-forecast"))
            {
                var local = DateTimeOffset.Now;
                StateStore.Write("sleep-forecast", new
                {
                    minuteOfDay = local.Hour * 60 + local.Minute,
                    epochMinute = local.ToUnixTimeSeconds() / 60
                });
            }

            await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
        }
    }
}
