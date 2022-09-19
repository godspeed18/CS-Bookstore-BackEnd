using ITPLibrary.Api.Data.Entities;

namespace ITPLibrary.Api.Data.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        public Task<bool> PostOrderItem(OrderItem item);
    }
}
