using ITPLibrary.Domain.Entites;

namespace ITPLibrary.Application.Contracts.Persistance
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        public  Task<IEnumerable<Order>> GetOrders(int userId);
        public  Task<Order> GetOrder(int orderId);
    }
}
