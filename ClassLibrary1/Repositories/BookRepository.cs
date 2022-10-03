using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Application.Validation.ValidationConstants;
using ITPLibrary.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace ITPLibrary.Infrastructure.Persistance.Repositories
{
    public class BookRepository : BaseAsyncRepository<Book>, IBookRepository
    {
        public BookRepository(ITPLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Book>> GetAllWithDetailsAsync()
        {
            return await _db.Books.Include(b => b.BookDetails).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetPopularAndRecentlyAddedBooks()
        {
            return await _db.Books
                .Where(u => (u.AddedDateTime.AddDays(BookValidationRules.RecentlyAddedRule)).CompareTo(DateTimeOffset.UtcNow) >= 0 || u.Popular == true)
                 .ToListAsync();

        }

        public async Task<IEnumerable<Book>> GetPopularBooks()
        {
            return await _db.Books.Where(b => b.Popular == true).Include(b => b.BookDetails).ToListAsync();
        }
    }
}
