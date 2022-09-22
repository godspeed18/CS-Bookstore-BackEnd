using ITPLibrary.Api.Core.Dtos;
using Stripe;

namespace ITPLibrary.Api.Core.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<bool> PostOrder(OrderPostDto newOrder, int userId);
        public Task<IEnumerable<OrderDisplayDto>> GetAllOrders(int userId);
        public Task<bool> UpdateOrder(UpdateOrderDto updatedOrder);
        public Task<Charge> ProcessPayment(CreditCardDto creditCard, int userId);
    }
}

