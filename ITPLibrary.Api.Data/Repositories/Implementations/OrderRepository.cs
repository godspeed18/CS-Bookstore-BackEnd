using ITPLibrary.Api.Data.Data;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITPLibrary.Api.Data.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task PostOrder(Order newOrder)
        {
            await _db.Orders.AddAsync(newOrder);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetOrders(int userId)
        {
            return await _db.Orders.Where(u => u.UserId == userId)
                    .Include(d => d.Items).ToListAsync();
        }

        public async Task UpdateOrder(Order updatedOrder)
        {
            _db.Orders.Update(updatedOrder);
            await _db.SaveChangesAsync();
        }

        public async Task<Order> GetOrder(int orderId)
        {
            return await _db.Orders.Where(u => u.Id == orderId).FirstOrDefaultAsync();
        }
    }
}
