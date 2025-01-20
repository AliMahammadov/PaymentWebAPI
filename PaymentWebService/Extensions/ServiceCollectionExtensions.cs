using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentWebData.Extensions;

namespace PaymentWebService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.LoadDataLayerExtension(configuration);
            services.LoadServiceLayerExtension();
            return services;
        }
    }
}