using Common;
using ITPLibrary.Api.Controllers.MethodRoutes;
using ITPLibrary.Api.Data.Entities.RequestMessages;
using ITPLibrary.Application.Features.ShoppingCarts.Commands;
using ITPLibrary.Application.Features.ShoppingCarts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITPLibrary.Api.Controllers
{
    [Authorize]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingCartController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpDelete($"{ShoppingCartControllerRoutes.DeleteItem}/{{bookId}}")]
        public async Task<ActionResult> DeleteItem([FromRoute]int bookId)
        {
            int userId = CommonMethods.GetUserIdFromContext(_httpContextAccessor.HttpContext);
            
            DeleteBookFromShoppingCartCommand deleteBook = new DeleteBookFromShoppingCartCommand();
            deleteBook.BookId = bookId;
            deleteBook.UserId = userId;

            var serviceResponse = await _mediator.Send(deleteBook);
            if (serviceResponse == null)
            {
                return BadRequest(ShoppingCartMessages.FailedToDeleteFromCart);
            }

            return Ok(ShoppingCartMessages.Success);
        }

        [HttpPost($"{ShoppingCartControllerRoutes.AddItem}/{{bookId}}")]
        public async Task<ActionResult> PostItem([FromRoute] int bookId)
        {
            int userId = CommonMethods.GetUserIdFromContext(_httpContextAccessor.HttpContext);

            AddBookToShoppingCartCommand addBook = new AddBookToShoppingCartCommand();
            addBook.BookId = bookId;
            addBook.UserId = userId;

            var serviceResponse = await _mediator.Send(addBook);

            if (serviceResponse == null)
            {
                return BadRequest(ShoppingCartMessages.FailedToAddToCart);
            }

            return Ok(ShoppingCartMessages.Success);
        }

   

        [HttpGet(ShoppingCartControllerRoutes.GetShoppingCart)]
        public async Task<ActionResult> GetShoppingCart()
        {
            int userId = CommonMethods.GetUserIdFromContext(_httpContextAccessor.HttpContext);

            GetShoppingCartQuery getCart = new GetShoppingCartQuery();
            getCart.UserId = userId;

            var shoppingCart = await _mediator.Send(getCart);

            return Ok(shoppingCart);
        }
    }
}
