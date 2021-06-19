using App.Metrics;
using Domain.Configurations;
using Domain.Providers;
using Infrastructure.CrossCutting.InfluxDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CrossCutting.Metrics
{
    public static class MetricsModule
    {
        public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
        {
            var influxDbConfiguration = configuration.GetSection("InfluxDbConfiguration").Get<InfluxDbConfiguration>();

            var metrics = AppMetrics.CreateDefaultBuilder()
                .Configuration
                .Configure(options =>
                {
                    options.DefaultContextLabel = configuration["MetricsOptions:DefaultContextLabel"];
                    options.AddEnvTag(configuration["MetricsOptions:GlobalTags:env"]);
                })
                .Report.ToInfluxDb(options =>
                {
                    InfluxDbConnection.SetConfigurations(options, influxDbConfiguration);
                })
                .Build();

            services.AddSingleton(metrics);
            services.AddTransient<IMetricsProvider, MetricsProvider>();

            return services;
        }
    }
}