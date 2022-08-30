using ITPLibrary.Api.Data.Entities;

namespace ITPLibrary.Api.Data.Data.Data_Provider.Interfaces
{
    public interface IBookDataProvider
    {
        public Task<IEnumerable<Book>> GetPopularBooks();
    }
}
