using Common;
using ITPLibrary.Api.Controllers.MethodRoutes;
using ITPLibrary.Api.Data.Entities.RequestMessages;
using ITPLibrary.Application.Features.Orders.Commands;
using ITPLibrary.Application.Features.Orders.Queries;
using ITPLibrary.Application.Features.Orders.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITPLibrary.Api.Controllers
{
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor; 
        }

        [HttpPost(OrderControllerRoutes.PostOrder)]
        public async Task<ActionResult> PostOrder(OrderPostVm newOrder)
        {
            PostOrderCommand postOrderCommand = new PostOrderCommand();
            postOrderCommand.NewOrder = newOrder;
            postOrderCommand.UserId = CommonMethods.GetUserIdFromContext(_httpContextAccessor.HttpContext);

            int userId = CommonMethods.GetUserIdFromContext(HttpContext);
            if (await _mediator.Send(postOrderCommand) == null)
            {
                return BadRequest(OrderMessages.OrderNotPlaced);
            }

            return Ok(OrderMessages.Success);
        }

        [HttpGet(OrderControllerRoutes.GetAllOrders)]
        public async Task<ActionResult> GetAllOrders()
        {
            int userId = CommonMethods.GetUserIdFromContext(HttpContext);
            DisplayAllOrdersQuery displayAllOrdersQuery = new DisplayAllOrdersQuery();
            displayAllOrdersQuery.UserId=userId;

            var orders = await _mediator.Send(displayAllOrdersQuery);

            if (orders == null)
            {
                return BadRequest();
            }

            return Ok(orders);
        }

        [HttpPut(OrderControllerRoutes.UpdateOrder)]
        public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderVm updatedOrder)
        {
            int userId = CommonMethods.GetUserIdFromContext(HttpContext);
            
            UpdateOrderCommand updateOrderCommand = new UpdateOrderCommand();
            updateOrderCommand.UserId = userId;
            updateOrderCommand.UpdateOrderInfo = updatedOrder;

            var updateResponse = await _mediator.Send(updateOrderCommand);
            
            if (updateResponse == null)
            {
                return BadRequest(OrderMessages.OrderNotUpdated);
            }

            return Ok(OrderMessages.Success);
        }

        /*
        [HttpPost(OrderControllerRoutes.Checkout)]
        public async Task<ActionResult> Checkout(CreditCardDto userCreditCard)
        {
            var charge = await _orderService.ProcessPayment(userCreditCard, CommonMethods.GetUserIdFromContext(HttpContext));

            return Ok(charge.Status);
        }*/
    }
}
