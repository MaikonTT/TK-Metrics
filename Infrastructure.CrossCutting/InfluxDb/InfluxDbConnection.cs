using App.Metrics.Reporting.InfluxDB;
using Domain.Configurations;
using System;

namespace Infrastructure.CrossCutting.InfluxDb
{
    public static class InfluxDbConnection
    {
        public static void SetConfigurations(MetricsReportingInfluxDbOptions options, InfluxDbConfiguration configuration)
        {
            options.InfluxDb.BaseUri = new Uri(configuration.Url);
            options.InfluxDb.Database = configuration.Database;
            options.InfluxDb.UserName = configuration.User;
            options.InfluxDb.Password = configuration.Password;
            options.InfluxDb.CreateDataBaseIfNotExists = true;
            options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
            options.HttpPolicy.FailuresBeforeBackoff = 5;
            options.FlushInterval = TimeSpan.FromSeconds(10);
        }
    }
}