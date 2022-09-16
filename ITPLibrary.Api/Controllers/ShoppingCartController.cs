using ITPLibrary.Api.Controllers.MethodRoutes;
using ITPLibrary.Api.Core.GenericConstants;
using ITPLibrary.Api.Core.Services.Interfaces;
using ITPLibrary.Api.Data.Entities.RequestMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpPost(ShoppingCartControllerRoutes.AddItem)]
        public async Task<ActionResult> PostItem([FromRoute] int bookId)
        {
            var serviceResponse = await _service.PostBookInCart(GetUserIdFromToken(), bookId);

            if (serviceResponse != true)
            {
                return BadRequest(ShoppingCartMessages.FailedToAddToCart);
            }

            return Ok(ShoppingCartMessages.Success);
        }

        [HttpGet(ShoppingCartControllerRoutes.GetShoppingCart)]
        public async Task<ActionResult> GetShoppingCart()
        {
            var shoppingCart = await _service.GetShoppingCart(GetUserIdFromToken());

            return Ok(shoppingCart);
        }

        private int GetUserIdFromToken()
        {
            var userIdentity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = int.Parse(userIdentity.FindFirst(GenericConstant.IdClaim).Value);
            return userId;
        }
    }
}
