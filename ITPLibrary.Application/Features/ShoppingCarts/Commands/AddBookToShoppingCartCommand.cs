using ITPLibrary.Application.Features.ShoppingCarts.ViewModels;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.ShoppingCarts.Commands
{
    public class AddBookToShoppingCartCommand : IRequest<ShoppingCart>
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
