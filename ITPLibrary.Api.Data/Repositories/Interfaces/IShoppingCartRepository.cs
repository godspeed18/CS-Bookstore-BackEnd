using ITPLibrary.Api.Data.Entities;

namespace ITPLibrary.Api.Data.Repositories.Interfaces
{
    public interface IShoppingCartRepository
    {
        public Task PostNewBookInCart(ShoppingCart newBookInCart);
        public Task<bool> IncrementBookQuantityInCart(int userId, int bookId);
        public Task<ShoppingCart> GetBookFromCart(int userId, int bookId);
        public Task<bool> DeleteBookFromCart(int userId, int bookId);
        public Task<bool> DecrementBookQuantityInCart(int userId, int bookId);
        public Task<int> GetBookQuantity(int userId, int bookId); 
        public Task<IEnumerable<ShoppingCart>> GetUserShoppingCart(int userId);
        public Task EmptyCart(int userId);
    }
}
