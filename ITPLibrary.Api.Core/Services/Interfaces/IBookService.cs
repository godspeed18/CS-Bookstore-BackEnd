using ITPLibrary.Api.Data.Entities;

namespace ITPLibrary.Api.Core.Services.Interfaces
{
    public interface IBookService
    {
        public IEnumerable<Book> GetPopularBooks();
    }
}
