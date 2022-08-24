using ITPLibrary.Api.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ITPLibrary.Api.Core.Services.Interfaces
{
    public interface IBookService
    {
        public Task<ActionResult<IEnumerable<BookDto>>> GetPopularBooks();
        public Task<ActionResult<BookDto>> GetBookById(int id);
        public Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks();
    }
}
