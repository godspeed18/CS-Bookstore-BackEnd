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

        public async Task<bool> UpdateOrder(UpdateOrderDto updatedOrder)
        {
            Order unchangedOrder = await _orderRepository.GetOrder(updatedOrder.Id);

            if (unchangedOrder == null)
            {
                return false;
            }

            if (unchangedOrder.OrderStatusId == (int)OrderStatusEnum.New)
            {
                unchangedOrder = await UpdateNewOrder(updatedOrder, unchangedOrder);
            }

            if (unchangedOrder.OrderStatusId == (int)OrderStatusEnum.Processing)
            {
                unchangedOrder = await UpdateProcessingOrder(updatedOrder, unchangedOrder);
            }

            if (unchangedOrder.OrderStatusId == (int)OrderStatusEnum.Dispatched)
            {
                unchangedOrder = await UpdateDispatchedOrder(updatedOrder, unchangedOrder);
            }

            await _orderRepository.UpdateOrder(unchangedOrder);
            return true;
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

        private async Task<Order> UpdateNewOrder(UpdateOrderDto updatedOrder, Order unchangedOrder)
        {
            if (updatedOrder.BillingAddress != null)
            {
                await UpdateBillingAddress(unchangedOrder, updatedOrder.BillingAddress);
            }

            if (updatedOrder.DeliveryAddress != null)
            {
                await UpdateDeliveryAddress(unchangedOrder, updatedOrder.DeliveryAddress);
            }

            if (updatedOrder.Observations != null)
            {
                unchangedOrder.Observations = updatedOrder.Observations;
            }

            if (updatedOrder.PaymentType != null)
            {
                unchangedOrder.PaymentTypeId = (int)updatedOrder.PaymentType;
            }

            return unchangedOrder;
        }

        private async Task<Order> UpdateProcessingOrder(UpdateOrderDto updatedOrder, Order unchangedOrder)
        {
            if (updatedOrder.BillingAddress.PhoneNumber != null)
            {
                unchangedOrder.BillingAddress.PhoneNumber = updatedOrder.BillingAddress.PhoneNumber;
                await _orderRepository.UpdateAddress(unchangedOrder.BillingAddress);
            }

            if (updatedOrder.DeliveryAddress != null)
            {
                await UpdateDeliveryAddress(unchangedOrder, updatedOrder.DeliveryAddress);
            }

            if (updatedOrder.Observations != null)
            {
                unchangedOrder.Observations = updatedOrder.Observations;
            }

            if (updatedOrder.PaymentType != null)
            {
                unchangedOrder.PaymentTypeId = (int)updatedOrder.PaymentType;
            }

            return unchangedOrder;
        }

        private async Task<Order> UpdateDispatchedOrder(UpdateOrderDto updatedOrder, Order unchangedOrder)
        {
            if (updatedOrder.DeliveryAddress.PhoneNumber != null)
            {
                unchangedOrder.DeliveryAddress.PhoneNumber = updatedOrder.DeliveryAddress.PhoneNumber;
                await _orderRepository.UpdateAddress(unchangedOrder.DeliveryAddress);
            }

            if (updatedOrder.Observations != null)
            {
                unchangedOrder.Observations = updatedOrder.Observations;
            }

            return unchangedOrder;
        }

        private async Task UpdateBillingAddress(Order order, Address newAddress)
        {
            Address updatedAddress = order.BillingAddress;

            if (newAddress.AddressLine != null)
            {
                updatedAddress.AddressLine = newAddress.AddressLine;
            }

            if (newAddress.PhoneNumber != null)
            {
                updatedAddress.PhoneNumber = newAddress.PhoneNumber;
            }

            if (newAddress.Country != null)
            {
                updatedAddress.Country = newAddress.Country;
            }

            await _orderRepository.UpdateAddress(updatedAddress);
        }

        private async Task UpdateDeliveryAddress(Order order, Address newAddress)
        {
            Address updatedAddress = order.DeliveryAddress;

            if (newAddress.AddressLine != null)
            {
                updatedAddress.AddressLine = newAddress.AddressLine;
            }

            if (newAddress.PhoneNumber != null)
            {
                updatedAddress.PhoneNumber = newAddress.PhoneNumber;
            }

            if (newAddress.Country != null)
            {
                updatedAddress.Country = newAddress.Country;
            }

            await _orderRepository.UpdateAddress(updatedAddress);
        }
    }
}
