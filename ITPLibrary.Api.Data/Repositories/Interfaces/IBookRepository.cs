using ITPLibrary.Api.Data.Entities;

namespace ITPLibrary.Api.Data.Repositories.Interfaces
{
    public interface IBookRepository
    {
        public Task<IEnumerable<Book>> GetPopularBooks();
        public Task<IEnumerable<Book>> GetAllBooks();
        public Task<Book> GetBookById(int id);
    }
}
