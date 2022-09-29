using ITPLibrary.Api.Core.Configurations;
using ITPLibrary.Api.Data.Configurations;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace ITPLibrary.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
           
            var jwtConfig = new JwtConfiguration();
            ConfigurationBinder.Bind(configuration, jwtConfig);
            services.AddSingleton(jwtConfig);

            var passwordRecoveryConfig = new PasswordRecoveryConfiguration();
            ConfigurationBinder.Bind(configuration, passwordRecoveryConfig);
            services.AddSingleton(passwordRecoveryConfig);

            var portAndHostConfig = new PortAndHostConfiguration();
            ConfigurationBinder.Bind(configuration, portAndHostConfig);
            services.AddSingleton(portAndHostConfig);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidAudience = jwtConfig.Audience,
                    ValidIssuer = jwtConfig.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtConfig.Key))
                };
            });

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
