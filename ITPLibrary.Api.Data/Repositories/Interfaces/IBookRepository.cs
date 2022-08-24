using ITPLibrary.Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ITPLibrary.Api.Data.Repositories.Interfaces
{
    public interface IBookRepository
    {
        public Task<ActionResult<IEnumerable<Book>>> GetPopularBooks();
        public Task<ActionResult<IEnumerable<Book>>> GetAllBooks();
        public Task<ActionResult<Book>> GetBookById(int id);
    }
}
