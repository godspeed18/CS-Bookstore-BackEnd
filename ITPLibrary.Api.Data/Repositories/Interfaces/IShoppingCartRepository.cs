using ITPLibrary.Api.Data.Entities;

namespace ITPLibrary.Api.Data.Repositories.Interfaces
{
    public interface IShoppingCartRepository
    {
        public Task PostNewBookInCart(ShoppingCart newBookInCart);
        public Task<bool> IncrementBookQuantityInCart(int userId, int bookId);
        public Task<ShoppingCart> GetBookFromCart(int userId, int bookId);
    }
}
