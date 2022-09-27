using ITPLibrary.Domain.Entites;

namespace ITPLibrary.Application.Contracts.Persistance
{
    public interface IShoppingCartRepository : IAsyncRepository<ShoppingCart>
    {
        public Task<ShoppingCart> GetAsync(int userId,int bookId);
    }
}
