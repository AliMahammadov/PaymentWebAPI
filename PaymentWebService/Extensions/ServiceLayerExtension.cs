using Microsoft.Extensions.DependencyInjection;
using PaymentWebService.Services.Abstraction;
using PaymentWebService.Services.Concrete;

namespace PaymentWebService.Extensions
{
    public static class ServiceLayerExtension
    {
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IBalanceServices, BalanceService>();
            services.AddScoped<TokenService>();


            return services;
        }
    }
}
