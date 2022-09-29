﻿using AutoMapper;
using Common;
using ITPLibrary.Api.Data.Entities.Enums;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Application.Features.Orders.ViewModels;
using ITPLibrary.Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ITPLibrary.Application.Features.Orders.Commands
{
    public class PostOrderCommandHandler : IRequestHandler<PostOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
       
        public PostOrderCommandHandler
            (IOrderRepository orderRepository,
                IOrderItemRepository orderItemRepository,
                    IShoppingCartRepository shoppingCartRepository,
                        IMapper mapper,
                            IHttpContextAccessor httpContextAccessor)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Order> Handle(PostOrderCommand request, CancellationToken cancellationToken)
        {
            int userId = CommonMethods.GetUserIdFromContext(_httpContextAccessor.HttpContext);
            
            if (request.NewOrder == null || userId==0)
            {
                return null;
            }

            var productList = await _shoppingCartRepository.GetUserShoppingCart(userId);
            if (productList == null)
            {
                return null;
            }

            int totalPrice = CalculateOrderPrice(productList);
            var mappedOrder = MapOrder(request.NewOrder, userId, totalPrice);
            await _orderRepository.AddAsync(mappedOrder);

            if (mappedOrder.Id == 0)
            {
                return null;
            }

            await PostOrderItems(productList, mappedOrder.Id);
            await _shoppingCartRepository.EmptyCart(userId);
            await _shoppingCartRepository.SaveChangesAsync();

            return mappedOrder;
        }

        private Order MapOrder(OrderPostVm order, int userId, int totalPrice)
        {
            var mappedNewOrder = _mapper.Map<Order>(order);
            mappedNewOrder.UserId = userId;
            mappedNewOrder.OrderStatusId = (int)OrderStatusEnum.New;
            mappedNewOrder.TotalPrice = totalPrice;

            return mappedNewOrder;
        }

        private static int CalculateOrderPrice(IEnumerable<ShoppingCart> productList)
        {
            int totalPrice = 0;
            foreach (var product in productList)
            {
                totalPrice += product.Quantity * product.Book.Price;
            }

            return totalPrice;
        }

        private async Task PostOrderItems(IEnumerable<ShoppingCart> itemList, int orderId)
        {
            foreach (var shoppingCartItem in itemList)
            {
                await _orderItemRepository.AddAsync(MapShoppingCartToOrderItem(shoppingCartItem, orderId));
            }
        }

        private OrderItem MapShoppingCartToOrderItem(ShoppingCart item, int orderId)
        {
            var orderItem = _mapper.Map<OrderItem>(item);
            orderItem.OrderId = orderId;
            return orderItem;
        }
    }
}
