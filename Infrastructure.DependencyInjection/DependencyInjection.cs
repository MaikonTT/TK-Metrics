using Infrastructure.CrossCutting.Metrics;
using Infrastructure.DependencyInjection.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection GetModules(IServiceCollection services, IConfiguration configuration)
        {
            DomainModule.Register(services, configuration);
            MetricsModule.Register(services, configuration);

            return services;
        }
    }
}