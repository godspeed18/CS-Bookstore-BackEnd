using ITPLibrary.Application.Features.Orders.ViewModels;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.Orders.Commands
{
    public class PostOrderCommand : IRequest<Order>
    {
        public OrderPostVm NewOrder { get; set; }
        public int UserId { get; set; }
    }
}
