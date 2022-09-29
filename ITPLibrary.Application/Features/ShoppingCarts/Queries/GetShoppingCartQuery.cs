using ITPLibrary.Application.Features.ShoppingCarts.ViewModels;
using MediatR;

namespace ITPLibrary.Application.Features.ShoppingCarts.Queries
{
    public class GetShoppingCartQuery : IRequest<IReadOnlyList<DisplayShoppingCartVm>>
    {
    }
}
