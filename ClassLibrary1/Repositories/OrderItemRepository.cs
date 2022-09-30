using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;

namespace ITPLibrary.Infrastructure.Persistance.Repositories
{
    public class OrderItemRepository : BaseAsyncRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ITPLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
