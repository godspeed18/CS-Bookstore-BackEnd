using ITPLibrary.Api.Controllers.Method_Routes;
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

        [HttpGet(BookControllerRoutes.GetAllBooks)]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();

            if (books.Value != null)
            {
                return Ok(books);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet(BookControllerRoutes.GetPopularBooks)]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetPopularBooks()
        {
            var books = await _bookService.GetPopularBooks();

            if (books.Value != null)
            {
                return Ok(books);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet(BookControllerRoutes.GetBookById)]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            var book = await _bookService.GetBookById(id);
            if (book.Value != null)
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
