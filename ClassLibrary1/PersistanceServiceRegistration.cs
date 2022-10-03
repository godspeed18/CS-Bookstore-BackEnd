using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITPLibrary.Infrastructure.Persistance
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ITPLibraryDbContext>(options => options.UseSqlServer(
                 configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IRecoveryCodeRepository, RecoveryCodeRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseAsyncRepository<>));

            return services;
        }
    }
}
