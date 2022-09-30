using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;

namespace ITPLibrary.Infrastructure.Persistance.Repositories
{
    public class AddressRepository : BaseAsyncRepository<Address>, IAddressRepository
    {
        public AddressRepository(ITPLibraryDbContext dbContext) : base(dbContext)
        {

        }
    }
}
