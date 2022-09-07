using ITPLibrary.Api.Data.Entities;

namespace ITPLibrary.Api.Data.Repositories.Interfaces
{
    public interface IBookRepository
    {
        public Task<Book> GetBookDetails(int bookId);
        public Task<IEnumerable<Book>> GetPopularAndRecentlyAddedBooks();
        public Task<IEnumerable<Book>> GetPromotedBooks();
        public Task<IEnumerable<Book>> GetPopularBooks();
        public Task<IEnumerable<Book>> GetAllBooks();
        public Task<Book> GetBookById(int id);
        public Task PostBook(Book newBook);
    }
}
