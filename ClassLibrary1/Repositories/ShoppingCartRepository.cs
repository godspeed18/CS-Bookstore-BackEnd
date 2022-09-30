using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace ITPLibrary.Infrastructure.Persistance.Repositories
{
    public class ShoppingCartRepository : BaseAsyncRepository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ITPLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public async Task EmptyCart(int userId)
        {
            var shoppingCart = await _db.ShoppingCarts.Where(u => u.UserId == userId).ToListAsync();
            foreach (var item in shoppingCart)
            {
                _db.ShoppingCarts.Remove(item);
            }

            await _db.SaveChangesAsync();
        }

        public async Task<ShoppingCart> GetAsync(int userId, int bookId)
        {
            var bookInCart = await _db.ShoppingCarts.Where(u => u.UserId == userId && u.BookId == bookId).Include(u => u.Book).FirstOrDefaultAsync();
            return bookInCart;
        }

        public async Task<IEnumerable<ShoppingCart>> GetUserShoppingCart(int userId)
        {
            var shoppingCart = await _db.ShoppingCarts.Where(u => u.UserId == userId).Include(u => u.Book).Include(u => u.User).ToListAsync();
            return shoppingCart;
        }
    }
}
