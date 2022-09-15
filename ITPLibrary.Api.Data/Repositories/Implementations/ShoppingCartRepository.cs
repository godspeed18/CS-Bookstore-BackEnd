using ITPLibrary.Api.Data.Data;
using ITPLibrary.Api.Data.Repositories.Interfaces;

namespace ITPLibrary.Api.Data.Repositories.Implementations
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db)
        {
            _db = db;
        }
    }
}
