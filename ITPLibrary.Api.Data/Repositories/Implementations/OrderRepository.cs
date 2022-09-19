using ITPLibrary.Api.Data.Data;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Repositories.Interfaces;

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
    }
}
