using ITPLibrary.Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ITPLibrary.Api.Data.Data.Data_Provider.Interfaces
{
    public interface IBookDataProvider
    {
        public Task<IEnumerable<Book>> GetPopularBooks();
    }
}
