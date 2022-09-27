using ITPLibrary.Application.Features.ShoppingCarts.ViewModels;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.ShoppingCarts.Commands
{
    public class DeleteBookFromShoppingCartCommand : IRequest<ShoppingCart>
    {
        public ShoppingCartItemVm ShoppingCartItem { get; set; }
    }
}
