using ITPLibrary.Api.Data.Data;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITPLibrary.Api.Data.Repositories.Implementations
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;

        public ShoppingCartRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task PostNewBookInCart(ShoppingCart newBookInCart)
        {
            await _db.ShoppingCarts.AddAsync(newBookInCart);
            await _db.SaveChangesAsync();
        }

        public async Task<ShoppingCart> GetBookFromCart(int userId, int bookId)
        {
            var bookInCart = await _db.ShoppingCarts.Where(u => u.UserId == userId && u.BookId == bookId).FirstOrDefaultAsync();
            return bookInCart;
        }

        public async Task<bool> IncrementBookQuantityInCart(int userId, int bookId)
        {
            var bookInCart = await _db.ShoppingCarts.Where(u => u.UserId == userId && u.BookId == bookId).FirstOrDefaultAsync();

            if (bookInCart != null)
            {
                bookInCart.Quantity++;
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<ShoppingCart>> GetUserShoppingCart(int userId)
        {
            return await _db.ShoppingCarts.Where(u => u.UserId == userId).ToListAsync();
        }
    }
}
