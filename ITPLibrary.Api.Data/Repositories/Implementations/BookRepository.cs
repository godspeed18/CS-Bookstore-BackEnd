using ITPLibrary.Api.Data.Data;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.Validation_Rules;
using ITPLibrary.Api.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITPLibrary.Api.Data.Repositories.Implementations
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _db;

        public BookRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _db.Books.ToListAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _db.Books.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetPopularBooks()
        {
            return await _db.Books.ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetPromotedBooks()
        {
            return await _db.Books.Where(b => b.Popular == true).Include(b => b.BookDetails).ToListAsync();
        }

        public async Task PostBook(Book newBook)
        {
            await _db.Books.AddAsync(newBook);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetPopularAndRecentlyAddedBooks()
        {
            return await _db.Books.Where
                (u => (DateTimeOffset.UtcNow - u.AddedDateTime).TotalDays
                        <= BookValidationRules.RecentlyAddedRule
                            || u.Popular == true).ToListAsync();
        }

        public async Task<Book> GetBookDetails(int bookId)
        {
            return await _db.Books.Where(b => b.Id == bookId).Include(b => b.BookDetails).FirstOrDefaultAsync();
        }
    }
}