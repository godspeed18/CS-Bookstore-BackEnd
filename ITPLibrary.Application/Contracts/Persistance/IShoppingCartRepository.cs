using ITPLibrary.Domain.Entites;

namespace ITPLibrary.Application.Contracts.Persistance
{
    public interface IShoppingCartRepository : IAsyncRepository<ShoppingCart>
    {
    }
}
