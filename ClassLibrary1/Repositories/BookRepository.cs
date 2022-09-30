using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;

namespace ITPLibrary.Infrastructure.Persistance.Repositories
{
    public class BookRepository : BaseAsyncRepository<Book>, IBookRepository
    {
        public BookRepository(ITPLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public Task<IEnumerable<Book>> GetPopularAndRecentlyAddedBooks()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetPopularBooks()
        {
            throw new NotImplementedException();
        }
    }
}
