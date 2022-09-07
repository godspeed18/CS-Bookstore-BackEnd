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
            var promotedBooks = from Book in _db.Books
                                join BookDetails in _db.BookDetails
                                    on Book.Id equals BookDetails.BookId
                                select new Book
                                {
                                    Author = Book.Author,
                                    AddedDateTime = Book.AddedDateTime,
                                    Popular = Book.Popular,
                                    Title = Book.Title,
                                    Price = Book.Price,
                                    Id = Book.Id,
                                    Thumbnail = Book.Thumbnail,
                                    BookDetails = BookDetails
                                };

            return promotedBooks;
        }

        public async Task PostBook(Book newBook)
        {
            await _db.Books.AddAsync(newBook);
            await _db.SaveChangesAsync();
        }

        public async Task<BookDetails> GetBookDetails(int BookId)
        {
            return await _db.BookDetails.Where(u => u.BookId == BookId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Book>> GetPopularAndRecentlyAddedBooks()
        {
            return await _db.Books.Where
                (u => (DateTimeOffset.UtcNow - u.AddedDateTime).TotalDays
                        <= BookValidationRules.RecentlyAddedRule
                            || u.Popular == true).ToListAsync();
        }
    }
}