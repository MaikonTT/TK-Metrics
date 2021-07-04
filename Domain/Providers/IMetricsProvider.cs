using System;
using System.Threading.Tasks;

namespace Domain.Providers
{
    public interface IMetricsProvider
    {
        void InitializeConfig(string timerOptionsName);
        void InitializeConfig(string counterOptionsName, string timerOptionsName);
        void SetMetricTags(string[] keys, string[] values);
        T ExecuteTimerMeasureWithResult<T>(Func<T> action, string userValue);
        Task ExecuteTimerMeasureWithoutResult(Func<Task> action, string userValue);
        void IncrementCounterMeasure(string value);
        void InstanceWithRecord(long elapsedTime);
        Task RegisterMetrics();
    }
}