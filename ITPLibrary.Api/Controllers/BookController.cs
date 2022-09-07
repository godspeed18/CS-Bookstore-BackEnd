using ITPLibrary.Api.Controllers.Method_Routes;
using ITPLibrary.Api.Core;
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

        [HttpPost(BookControllerRoutes.PostBook)]
        public async Task<ActionResult> PostBook(IFormFile bookImage, PostBookDto newBook)
        {
            if (bookImage == null)
            {
                return BadRequest();
            }
            else
            {
                newBook.Thumbnail = await ImageConverter.FormFileToByteArray(bookImage);
                newBook.RecentlyAdded = DateTimeOffset.UtcNow;
                await _bookService.PostBook(newBook);
                return Ok();
            }
        }

        [HttpGet(BookControllerRoutes.GetAllBooks)]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();

            if (books != null)
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

            if (books != null)
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
            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet(BookControllerRoutes.GetPromotedBooks)]
        public async Task<ActionResult<PromotedBookDto>> GetPromotedBooks()
        {
            var book = await _bookService.GetPromotedBooks();
            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet(BookControllerRoutes.GetPopularAndRecentlyAddedBooks)]
        public async Task<ActionResult<PromotedBookDto>> GetPromotedAndRecentlyAddedBooks()
        {
            var book = await _bookService.GetPopularAndRecentlyAddedBooks();
            if (book != null)
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
