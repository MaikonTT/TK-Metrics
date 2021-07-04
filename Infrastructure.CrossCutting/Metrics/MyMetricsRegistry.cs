using App.Metrics;
using App.Metrics.Counter;
using App.Metrics.Timer;

namespace Infrastructure.CrossCutting.Metrics
{
    public static class MyMetricsRegistry
    {
        public static CounterOptions Counter(string name)
            => new()
            {
                Name = $"{name} Hits",
                MeasurementUnit = Unit.Calls
            };

        public static TimerOptions Timer(string name)
            => new()
            {
                Name = $"{name} Timer",
                MeasurementUnit = Unit.Requests,
                DurationUnit = TimeUnit.Milliseconds,
                RateUnit = TimeUnit.Milliseconds
            };

    }
}