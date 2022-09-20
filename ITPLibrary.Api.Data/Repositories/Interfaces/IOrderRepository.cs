using ITPLibrary.Api.Data.Entities;

namespace ITPLibrary.Api.Data.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public Task PostOrder(Order newOrder);
        public Task<IEnumerable<Order>> GetOrders(int userId);
    }
}
