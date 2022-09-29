using ITPLibrary.Application.Features.Orders.ViewModels;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.Orders.Commands
{
    public class UpdateOrderCommand : IRequest<Order>
    {
        public UpdateOrderVm UpdateOrderInfo { get; set; }
    }
}
