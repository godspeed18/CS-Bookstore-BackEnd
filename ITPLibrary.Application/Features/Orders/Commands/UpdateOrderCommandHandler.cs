using ITPLibrary.Api.Data.Entities.Enums;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Application.Features.Addresses.ViewModels;
using ITPLibrary.Application.Features.Orders.ViewModels;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.Orders.Commands
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAddressRepository _addressRepository;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IAddressRepository addressRepository)
        {
            _orderRepository = orderRepository;
            _addressRepository = addressRepository;
        }

        public async Task<Order> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            Order unchangedOrder = await _orderRepository.GetOrder(request.UpdateOrderInfo.Id);

            if (unchangedOrder.UserId != request.UserId)
            {
                return null;
            }

            if (unchangedOrder == null)
            {
                return null;
            }

            if (unchangedOrder.OrderStatusId == (int)OrderStatusEnum.New)
            {
                unchangedOrder = await UpdateNewOrder(request.UpdateOrderInfo, unchangedOrder);
            }

            if (unchangedOrder.OrderStatusId == (int)OrderStatusEnum.Processing)
            {
                unchangedOrder = await UpdateProcessingOrder(request.UpdateOrderInfo, unchangedOrder);
            }

            if (unchangedOrder.OrderStatusId == (int)OrderStatusEnum.Dispatched)
            {
                unchangedOrder = await UpdateDispatchedOrder(request.UpdateOrderInfo, unchangedOrder);
            }

            var updatedOrder = await _orderRepository.UpdateAsync(unchangedOrder);
            return updatedOrder;

        }

        private async Task<Order> UpdateNewOrder(UpdateOrderVm updatedOrder, Order unchangedOrder)
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

        private async Task<Order> UpdateProcessingOrder(UpdateOrderVm updatedOrder, Order unchangedOrder)
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

        private async Task<Order> UpdateDispatchedOrder(UpdateOrderVm updatedOrder, Order unchangedOrder)
        {
            if (updatedOrder.Observations != null)
            {
                unchangedOrder.Observations = updatedOrder.Observations;
            }

            return unchangedOrder;
        }

        private async Task UpdateBillingAddress(Order order, AddressVm newAddress)
        {
            order.BillingAddress.AddressLine = newAddress.AddressLine;
            order.BillingAddress.PhoneNumber = newAddress.PhoneNumber;
            order.BillingAddress.Country = newAddress.Country;

            await _addressRepository.UpdateAsync(order.BillingAddress);
        }

        private async Task UpdateDeliveryAddress(Order order, AddressVm newAddress)
        {
            order.DeliveryAddress.AddressLine = newAddress.AddressLine;
            order.DeliveryAddress.PhoneNumber = newAddress.PhoneNumber;
            order.DeliveryAddress.Country = newAddress.Country;

            await _addressRepository.UpdateAsync(order.DeliveryAddress);
        }
    }
}
