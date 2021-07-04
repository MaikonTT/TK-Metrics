using Domain.Configurations;
using Domain.Providers;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Middlewares
{
    public class MetricMiddleware
    {
        private readonly RequestDelegate _request;
        private readonly IEnumerable<MetricMiddlewareConfiguration> _configurations;
        private readonly IMetricsProvider _metrics;
        private readonly ITimerProvider _timer;
        private const string CounterOptionsName = "Hits";
        private const string TimerOptionsName = "Timer";
        private int _routeSla = 2000;

        public MetricMiddleware(RequestDelegate request, IEnumerable<MetricMiddlewareConfiguration> configurations, IMetricsProvider metrics, ITimerProvider timer)
        {
            _request = request;
            _configurations = configurations;
            _metrics = metrics;
            _timer = timer;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var method = context.Request.Method.ToLower();
            var routePath = context.Request.Path.Value.Replace("/", string.Empty).ToLower();
            var calledRoute = _configurations.FirstOrDefault(f => f.RoutePath == routePath);

            if (method == null || calledRoute == null)
            {
                await _request.Invoke(context);
                return;
            }

            _routeSla = calledRoute.RouteSla;
            _metrics.InitializeConfig($"{calledRoute.RouteName} {CounterOptionsName}",
                                      $"{calledRoute.RouteName} {TimerOptionsName}");

            _timer.StartNew();

            try
            {
                await _request.Invoke(context);
            }
            finally
            {
                _timer.Stop();
                _metrics.SetMetricTags(new[] { "method" }, new[] { method });
                _metrics.IncrementCounterMeasure(CounterOptionsName);

                await _metrics.RegisterMetrics();
            }
        }
    }
}