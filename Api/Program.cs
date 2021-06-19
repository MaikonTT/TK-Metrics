using App.Metrics;
using App.Metrics.AspNetCore;
using Domain.Configurations;
using Infrastructure.CrossCutting.InfluxDb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var configuration = GetConfiguration();
            var influxDbConfiguration = configuration.GetSection("InfluxDbConfiguration").Get<InfluxDbConfiguration>();

            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureMetricsWithDefaults(builder =>

                    {
                        builder.Report.ToInfluxDb(options =>
                        {
                            InfluxDbConnection.SetConfigurations(options, influxDbConfiguration);
                        });
                    });
                    webBuilder.UseMetrics();
                    webBuilder.UseStartup<Startup>();
                });
        }

        private static IConfiguration GetConfiguration()
            => new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsetting.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();
    }
}
