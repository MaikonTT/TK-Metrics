using Domain.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection.Modules
{
    public static class DomainModule
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            var influxDbConfiguration = configuration.GetSection("InfluxDbConfiguration").Get<InfluxDbConfiguration>();
            services.AddSingleton(influxDbConfiguration);
        }
    }
}