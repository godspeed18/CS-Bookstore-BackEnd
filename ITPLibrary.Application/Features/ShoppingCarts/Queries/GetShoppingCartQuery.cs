using ITPLibrary.Application.Features.ShoppingCarts.ViewModels;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.ShoppingCarts.Queries
{
    public class GetShoppingCartQuery : IRequest<IReadOnlyList<DisplayShoppingCartVm>>
    {
    }
}
