using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vilas.Template.Application.Common.Interfaces;
using Vilas.Template.Application.Common.Interfaces.Repository;
using Vilas.Template.Application.Common.Interfaces.Services;
using Vilas.Template.Application.Common.Security.CurrentUserProvider;
using Vilas.Template.Application.Common.Security.TokenGenerator;
using Vilas.Template.Infrastructure.Common.Persistence;
using Vilas.Template.Infrastructure.Sales.Persistence;
using Vilas.Template.Infrastructure.Security;
using Vilas.Template.Infrastructure.Security.CurrentUserProvider;
using Vilas.Template.Infrastructure.Security.PolicyEnforcer;
using Vilas.Template.Infrastructure.Security.TokenGenerator;
using Vilas.Template.Infrastructure.Security.TokenValidation;

namespace Vilas.Template.Infrastructure
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
