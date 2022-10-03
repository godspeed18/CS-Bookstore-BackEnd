using ITPLibrary.Domain.Entites;

namespace ITPLibrary.Application.Contracts.Persistance
{
    public interface IBookRepository : IAsyncRepository<Book>
    {
        public Task<IEnumerable<Book>> GetPopularBooks();
        public Task<IEnumerable<Book>> GetPopularAndRecentlyAddedBooks();
        public Task<IEnumerable<Book>> GetAllWithDetailsAsync();
    }
}
