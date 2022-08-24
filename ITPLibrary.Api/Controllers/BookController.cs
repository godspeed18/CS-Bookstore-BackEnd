using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ITPLibrary.Api.Controllers
{
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();

            if (books != default)
            {
                return Ok(books);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<ActionResult<IEnumerable<BookDto>>> GetPopularBooks()
        {
            var books = await _bookService.GetPopularBooks();
            
            if (books != default)
            {
                return Ok(books);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            var book = await _bookService.GetBookById(id);
            if (book != default)
            {
                return Ok(book);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
