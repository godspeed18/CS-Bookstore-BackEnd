using ITPLibrary.Api.Data.Data;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Repositories.Interfaces;

namespace ITPLibrary.Api.Data.Repositories.Implementations
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> PostOrderItem(OrderItem item)
        {
            if (item == null)
            {
                return false;
            }

            await _db.OrderItems.AddAsync(item);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
