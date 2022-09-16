using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Data.Entities;

namespace ITPLibrary.Api.Core.Services.Interfaces
{
    public interface IShoppingCartService
    {
        public Task<bool> PostBookInCart(int userId, int bookId);
        public Task<bool> DeleteBookFromCart(int userId, int bookId);
        public Task<IEnumerable<BookDisplayDto>> GetShoppingCart(int userId);
    }
}
