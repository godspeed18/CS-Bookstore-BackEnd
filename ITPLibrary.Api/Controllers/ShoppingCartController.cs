using Common;
using ITPLibrary.Api.Controllers.MethodRoutes;
using ITPLibrary.Api.Core.Services.Interfaces;
using ITPLibrary.Api.Data.Entities.RequestMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITPLibrary.Api.Controllers
{
    [Authorize]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _service;

        public ShoppingCartController(IShoppingCartService service)
        {
            _service = service;
        }

        [HttpDelete($"{ShoppingCartControllerRoutes.DeleteItem}/{{bookId}}")]
        public async Task<ActionResult> DeleteItem([FromRoute]int bookId)
        {
            var serviceResponse = await _service.DeleteBookFromCart(CommonMethods.GetUserIdFromContext
                                                (HttpContext), bookId);
            if (serviceResponse == false)
            {
                return BadRequest(ShoppingCartMessages.FailedToDeleteFromCart);
            }

            return Ok(ShoppingCartMessages.Success);
        }

        [HttpPost($"{ShoppingCartControllerRoutes.AddItem}/{{bookId}}")]
        public async Task<ActionResult> PostItem([FromRoute] int bookId)
        {
            var serviceResponse = await _service.PostBookInCart(CommonMethods.GetUserIdFromContext
                                                   (HttpContext), bookId);

            if (serviceResponse != true)
            {
                return BadRequest(ShoppingCartMessages.FailedToAddToCart);
            }

            return Ok(ShoppingCartMessages.Success);
        }

   

        [HttpGet(ShoppingCartControllerRoutes.GetShoppingCart)]
        public async Task<ActionResult> GetShoppingCart()
        {
            var shoppingCart = await _service.GetShoppingCart(
                        CommonMethods.GetUserIdFromContext(HttpContext));

            return Ok(shoppingCart);
        }
    }
}
