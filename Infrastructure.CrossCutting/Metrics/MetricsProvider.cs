using App.Metrics;
using App.Metrics.Counter;
using App.Metrics.Timer;
using Domain.Providers;
using System;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Metrics
{
    public class MetricsProvider : IMetricsProvider
    {
        private readonly IMetricsRoot _metrics;
        private MetricTags _tags;
        private CounterOptions _counterOptions;
        private TimerOptions _timerOptions;

        public MetricsProvider(IMetricsRoot metrics)
        {
            _metrics = metrics;
        }

        public void InitializeConfig(string timerOptionsName)
            => SetTimerOptions(timerOptionsName);

        public void InitializeConfig(string counterOptionsName, string timerOptionsName)
        {
            SetCounterOptions(counterOptionsName);
            SetTimerOptions(timerOptionsName);
        }

        public void SetMetricTags(string[] keys, string[] values)
            => _tags = new MetricTags(keys, values);

        public async Task<T> ExecuteTimerMeasureWithResult<T>(Func<Task<T>> action, string userValue)
        {
            using (_metrics.Measure.Timer.Time(_timerOptions, _tags, userValue))
            {
                return await action();
            }
        }

        public async Task ExecuteTimerMeasureWithoutResult(Func<Task> action, string userValue)
        {
            using (_metrics.Measure.Timer.Time(_timerOptions, _tags, userValue))
            {
                await action();
            }
        }

        public void IncrementCounterMeasure(string value)
            => _metrics.Measure.Counter.Increment(_counterOptions, _tags, value);

        public void InstanceWithRecord(long elapsedTime)
            => _metrics.Provider.Timer.Instance(_timerOptions, _tags).Record(elapsedTime, TimeUnit.Milliseconds);

        public async Task RegisterMetrics()
            => await Task.WhenAll(_metrics.ReportRunner.RunAllAsync());

        private void SetCounterOptions(string name)
            => _counterOptions = new CounterOptions
            {
                Name = name,
                MeasurementUnit = Unit.Calls,
                ResetOnReporting = true
            };

        private void SetTimerOptions(string name)
            => _timerOptions = new TimerOptions
            {
                Name = name,
                MeasurementUnit = Unit.Requests,
                DurationUnit = TimeUnit.Milliseconds,
                RateUnit = TimeUnit.Milliseconds
            };
    }
}