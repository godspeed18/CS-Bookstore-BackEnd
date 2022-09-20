using AutoMapper;
using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Core.Services.Interfaces;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.Enums;
using ITPLibrary.Api.Data.Repositories.Interfaces;

namespace ITPLibrary.Api.Core.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderService(IShoppingCartRepository shoppingCartRepository,
                IOrderRepository orderRepository,
                    IOrderItemRepository orderItemRepository,
                        IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<bool> PostOrder(OrderPostDto newOrder, int userId)
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

        public async Task<IEnumerable<OrderDisplayDto>> GetAllOrders(int userId)
        {
            var orders = await _orderRepository.GetOrders(userId);
            if (orders == null)
            {
                return null;
            }

            List<OrderDisplayDto> mappedOrders = _mapper.Map<List<OrderDisplayDto>>(orders);  
            return mappedOrders;
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
                await _orderItemRepository.PostOrderItem(MapToOrderItem(shoppingCartItem, orderId));
            }
        }

        private OrderItem MapToOrderItem(ShoppingCart item, int orderId)
        {
            var orderItem = _mapper.Map<OrderItem>(item);
            orderItem.OrderId = orderId;
            return orderItem;
        }

        private Order MapOrder(OrderPostDto order, int userId, int totalPrice)
        {
            var mappedNewOrder = _mapper.Map<Order>(order);
            mappedNewOrder.UserId = userId;
            mappedNewOrder.OrderStatusId = (int)OrderStatusEnum.New;
            mappedNewOrder.TotalPrice = totalPrice;

            return mappedNewOrder;
        }
    }
}
