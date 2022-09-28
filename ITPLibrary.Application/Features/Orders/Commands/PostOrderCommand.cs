using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.Orders.Commands
{
    public class PostOrderCommand : IRequest<Order>
    {
        public int UserId { get; set; }
    }
}
