using ITPLibrary.Application.Features.Orders.ViewModels;
using MediatR;

namespace ITPLibrary.Application.Features.Orders.Queries
{
    public class DisplayAllOrdersQuery : IRequest<IEnumerable<OrderDisplayVm>>
    {
    }
}
