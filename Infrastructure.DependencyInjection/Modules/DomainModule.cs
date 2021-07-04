using Domain.Configurations;
using Domain.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Infrastructure.DependencyInjection.Modules
{
    public static class DomainModule
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            var influxDbConfiguration = configuration.GetSection("InfluxDbConfiguration").Get<InfluxDbConfiguration>();
            services.AddSingleton(influxDbConfiguration);

            var metricMiddlewareConfiguration = configuration.GetSection("MetricMiddlewareConfiguration").Get<IEnumerable<MetricMiddlewareConfiguration>>();
            services.AddSingleton(metricMiddlewareConfiguration);

            services.AddTransient<ITimerProvider, TimerProvider>();
        }
    }
}