using AutoMapper;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Application.Features.Orders.ViewModels;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.Orders.Commands
{
    public class PostOrderCommandHandler : IRequestHandler<PostOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public PostOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<Order> Handle(PostOrderCommand request, CancellationToken cancellationToken)
        {
            
        }

        private async Task<bool> PostOrder(OrderPostVm newOrder, int userId)
        {
            if (newOrder == null)
            {
                return false;
            }

            var productList = await _shoppingCartRepository.GetUserShoppingCart(userId);
            if (productList == null)
            {
                return false;
            }

            int totalPrice = CalculateOrderPrice(productList);
            var mappedOrder = MapOrder(newOrder, userId, totalPrice);
            await _orderRepository.PostOrder(mappedOrder);
            if (mappedOrder.Id == 0)
            {
                return false;
            }

            await PostOrderItems(productList, mappedOrder.Id);
            await _shoppingCartRepository.EmptyCart(userId);
            return true;
        }

        private Order MapOrder(OrderPostVm order, int userId, int totalPrice)
        {
            var mappedNewOrder = _mapper.Map<Order>(order);
            mappedNewOrder.UserId = userId;
            mappedNewOrder.OrderStatusId = (int)OrderStatusEnum.New;
            mappedNewOrder.TotalPrice = totalPrice;

            return mappedNewOrder;
        }
    }
}
