using AutoMapper;
using Common;
using Constants;
using ITPLibrary.Api.Core.Configurations;
using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Core.Services.Interfaces;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.Enums;
using ITPLibrary.Api.Data.Entities.RequestMessages;
using ITPLibrary.Api.Data.Repositories.Interfaces;
using Stripe;

namespace ITPLibrary.Api.Core.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IUserRepository _userRepository;
        private readonly PaymentConfiguration _stripeConfig;

        public OrderService(IShoppingCartRepository shoppingCartRepository,
                IOrderRepository orderRepository,
                    IOrderItemRepository orderItemRepository,
                        IUserRepository userRepository,
                            PaymentConfiguration stripeConfig,
                                IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderItemRepository = orderItemRepository;
            _stripeConfig = stripeConfig;
            _userRepository = userRepository;
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

        public async Task<Charge> ProcessPayment(CreditCardDto creditCard, int userId)
        {
            var shoppingCart = await _shoppingCartRepository.GetUserShoppingCart(userId);
            if (shoppingCart == null)
            {
                return null;
            }

            StripeConfiguration.ApiKey = _stripeConfig.SecretKey;
            TokenCardOptions cardOptions = CreateCardOptions(creditCard);
            Token newToken = GetStripeToken(cardOptions);
            Customer customer = await CreateCustomer(creditCard.Email, newToken);
            Charge charge = await CreateChargeObject(customer, shoppingCart, userId);

            return charge;
        }

        private async Task<Charge> CreateChargeObject(Customer customer, IEnumerable<ShoppingCart> shoppingCart, int userId)
        {
            var options = new ChargeCreateOptions
            {
                Amount = CalculateOrderPrice(shoppingCart) * 100,
                Currency = OrderMessages.RON,
                Description = OrderMessages.CheckoutMessage,
                Source = customer.DefaultSourceId,
                ReceiptEmail = customer.Email,
                Metadata = await AddPaymentMetadata(shoppingCart, userId),
                Customer = customer.Id
            };

            var service = new ChargeService();
            Charge charge = service.Create(options);
            return charge;
        }

        private TokenCardOptions CreateCardOptions(CreditCardDto creditCard)
        {
            TokenCardOptions card = new TokenCardOptions();
            card.Name = $"{creditCard.FirstName} {creditCard.LastName}";
            card.Number = creditCard.CardNumber;
            card.ExpYear = creditCard.ExpirationYear.ToString();
            card.ExpMonth = creditCard.ExpirationMonth.ToString();
            card.Cvc = creditCard.CVV2.ToString();

            return card;
        }

        private Token GetStripeToken(TokenCardOptions card)
        {
            TokenCreateOptions token = new TokenCreateOptions();
            token.Card = card;
            TokenService serviceToken = new TokenService();
            Token newToken = serviceToken.Create(token);

            return newToken;
        }

        private async Task<Customer> CreateCustomer(string customerEmail, Token token)
        {
            CustomerCreateOptions customer = new CustomerCreateOptions()
            {
                Email = customerEmail,
                Source = token.Id,
                Name = token.Card.Name
            };

            var customerService = new CustomerService();
            Customer stripeCustomer = customerService.Create(customer);

            return stripeCustomer;
        }

        private async Task<Dictionary<string, string>> AddPaymentMetadata(IEnumerable<ShoppingCart> shoppingCart, int userId)
        {
            Dictionary<string, string> metadata = new Dictionary<string, string>();
            int productIndex = 1;
            foreach (var item in shoppingCart)
            {
                metadata.Add($"{CommonConstants.Product} {productIndex}", item.Book.Title);
                var bookQuanity = await _shoppingCartRepository.GetBookQuantity(userId, item.Book.Id);
                metadata.Add($"{CommonConstants.Quantity} of product {productIndex}", bookQuanity.ToString());

                productIndex++;
            }

            return metadata;
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
            if (updatedOrder.Observations != null)
            {
                unchangedOrder.Observations = updatedOrder.Observations;
            }

            return unchangedOrder;
        }

        private async Task UpdateBillingAddress(Order order, AddressDto newAddress)
        {
            order.BillingAddress.AddressLine = newAddress.AddressLine;
            order.BillingAddress.PhoneNumber = newAddress.PhoneNumber;
            order.BillingAddress.Country = newAddress.Country;

            await _orderRepository.UpdateAddress(order.BillingAddress);
        }

        private async Task UpdateDeliveryAddress(Order order, AddressDto newAddress)
        {
            order.DeliveryAddress.AddressLine = newAddress.AddressLine;
            order.DeliveryAddress.PhoneNumber = newAddress.PhoneNumber;
            order.DeliveryAddress.Country = newAddress.Country;

            await _orderRepository.UpdateAddress(order.DeliveryAddress);
        }
    }
}
