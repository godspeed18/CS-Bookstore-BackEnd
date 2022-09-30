using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace ITPLibrary.Infrastructure.Persistance.Repositories
{
    public class OrderRepository : BaseAsyncRepository<Order>, IOrderRepository
    {
        public OrderRepository(ITPLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Order> GetOrder(int orderId)
        {
            return await 
                _db.Orders.Where(u => u.Id == orderId)
                .Include(u => u.BillingAddress)
                .Include(u => u.DeliveryAddress)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrders(int userId)
        {
            return await _db.Orders.Where(u => u.UserId == userId)
                    .Include(d => d.Items).ToListAsync();
        }
    }
}
