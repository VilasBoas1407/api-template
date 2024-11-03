using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Tech.Test.Payment.Application.Common.Interfaces;
using Tech.Test.Payment.Application.Common.Interfaces.Repository;
using Tech.Test.Payment.Application.Common.Interfaces.Services;
using Tech.Test.Payment.Application.Common.Security.CurrentUserProvider;
using Tech.Test.Payment.Application.Common.Security.TokenGenerator;
using Tech.Test.Payment.Infrastructure.Common.Persistence;
using Tech.Test.Payment.Infrastructure.Sales.Persistence;
using Tech.Test.Payment.Infrastructure.Security;
using Tech.Test.Payment.Infrastructure.Security.CurrentUserProvider;
using Tech.Test.Payment.Infrastructure.Security.PolicyEnforcer;
using Tech.Test.Payment.Infrastructure.Security.TokenGenerator;
using Tech.Test.Payment.Infrastructure.Security.TokenValidation;

namespace Tech.Test.Payment.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHttpContextAccessor()
                .AddAuthorization()
                .AddPersistence(configuration)
                .AddAuthentication(configuration);

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("Context")));

            services.AddScoped<ISalesRepository, SalesRepository>();

            return services;
        }

        private static IServiceCollection AddAuthorization(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
            services.AddSingleton<IPolicyEnforcer, PolicyEnforcer>();

            return services;
        }


        private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<ICurrentUserProvider, CurrentUserProvider>();

            services
                .ConfigureOptions<JwtBearerTokenValidationConfiguration>()
                .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            return services;
        }


    }
}
