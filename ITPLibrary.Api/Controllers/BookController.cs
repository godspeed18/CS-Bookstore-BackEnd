using ITPLibrary.Api.Controllers.Method_Routes;
using ITPLibrary.Application.Features.Books.Commands;
using ITPLibrary.Application.Features.Books.Queries;
using ITPLibrary.Application.Features.Books.ViewModels;
using ITPLibrary.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITPLibrary.Api.Controllers
{
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(BookControllerRoutes.PostBook)]
        public async Task<ActionResult> PostBook(IFormFile bookImage, BookPostVm newBook)
        {
            if (bookImage == null)
            {
                return BadRequest();
            }
            else
            {
                newBook.Thumbnail = await ImageConverter.FormFileToByteArray(bookImage);
                newBook.RecentlyAdded = DateTimeOffset.UtcNow;
               
                PostBookCommand postBookCommand = new PostBookCommand();
                postBookCommand.Book = newBook;
                
                await _mediator.Send(postBookCommand);
                return Ok();
            }
        }

        [HttpGet(BookControllerRoutes.GetAllBooks)]
        public async Task<ActionResult<IEnumerable<BookWithDetailsVm>>> GetAllBooks()
        {
            var books = await _mediator.Send(new GetAllBooksQuery());

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
        public async Task<ActionResult<IEnumerable<PopularBookVm>>> GetPopularBooks()
        {
            var books = await _mediator.Send(new GetPopularBooksQuery());

            if (books != null)
            {
                return Ok(books);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet(BookControllerRoutes.GetPopularAndRecentlyAddedBooks)]
        public async Task<ActionResult<IEnumerable<RecentlyAddedAndPopularBookVm>>> GetPromotedAndRecentlyAddedBooks()
        {
            GetPopularAndRecentlyAddedBooksQuery getPopularAndRecently = new GetPopularAndRecentlyAddedBooksQuery();

            var book = await _mediator.Send(getPopularAndRecently);
            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet(BookControllerRoutes.GetBookById)]
        public async Task<ActionResult<BookWithDetailsVm>> GetBookById(int id)
        {
            GetBookDetailsQuery getBookDetails = new GetBookDetailsQuery();
            getBookDetails.Id = id;

            var book = await _mediator.Send(getBookDetails);

            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound();
            }
        }
        
        /*
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


        [HttpGet(BookControllerRoutes.GetBookDetails)]
        public async Task<ActionResult> GetBookDetails(int BookId)
        {
            var book = await _bookService.GetBookDetails(BookId);

            if (book == null)
            {
                return BadRequest("No book with the specified id was found");
            }
            else
            {
                return Ok(book);
            }
        }
        */
    }
}
