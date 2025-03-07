using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PaymentWebService.Authentication
{
    public static class JwtAuthenticationExtension
    {
        public static IServiceCollection ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var key = configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key is not provided");
            var issuer = configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer is not provided");
            var audience = configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience is not provided");
            var expireDays = configuration.GetValue<int>("Jwt:ExpireDays", 1);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = issuer,
                        ValidAudience = issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var token = context.Request.Headers["Bearer "].ToString();
                            if (!string.IsNullOrEmpty(token))
                            {
                                context.Token = token.Trim();
                            }
                            return Task.CompletedTask;
                        }
                    };


                });

            return services;
        }
    }
}
