using ITPLibrary.Application.Contracts.Infrastructure;
using ITPLibrary.Common;
using ITPLibrary.Infrastructure.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITPLibrary.Infrastructure.InfrastructureService
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddTransient<IEmailService, EmailService>();
            return services;
        }
    }
}
