using Common;
using ITPLibrary.Api.Controllers.MethodRoutes;
using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Core.Services.Interfaces;
using ITPLibrary.Api.Data.Entities.RequestMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITPLibrary.Api.Controllers
{
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost(OrderControllerRoutes.PostOrder)]
        public async Task<ActionResult> PostOrder(OrderPostDto newOrder)
        {
            int userId = CommonMethods.GetUserIdFromContext(HttpContext);
            if (await _orderService.PostOrder(newOrder, userId) == false)
            {
                return BadRequest(OrderMessages.OrderNotPlaced);
            }

            return Ok(OrderMessages.Success);
        }

        [HttpGet(OrderControllerRoutes.GetAllOrders)]
        public async Task<ActionResult> GetAllOrders()
        {
            int userId = CommonMethods.GetUserIdFromContext(HttpContext);
            var orders = await _orderService.GetAllOrders(userId);

            if (orders == null)
            {
                return BadRequest();
            }

            return Ok(orders);
        }

        [HttpPut(OrderControllerRoutes.UpdateOrder)]
        public async Task<ActionResult> UpdateOrder(UpdateOrderDto updatedOrder)
        {
            var updateResponse = await _orderService.UpdateOrder(updatedOrder);
            if(updateResponse == false)
            {
                return BadRequest(OrderMessages.OrderNotUpdated);
            }

            return Ok(OrderMessages.Success);
        }
    }
}
