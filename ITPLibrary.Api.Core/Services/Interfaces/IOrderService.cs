using ITPLibrary.Api.Core.Dtos;

namespace ITPLibrary.Api.Core.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<bool> PostOrder(OrderPostDto newOrder, int userId);
    }
}
