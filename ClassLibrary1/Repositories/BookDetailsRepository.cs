using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;

namespace ITPLibrary.Infrastructure.Persistance.Repositories
{
    public class BookDetailsRepository : BaseAsyncRepository<BookDetails>, IBookDetailsRepository
    {
        public BookDetailsRepository(ITPLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
