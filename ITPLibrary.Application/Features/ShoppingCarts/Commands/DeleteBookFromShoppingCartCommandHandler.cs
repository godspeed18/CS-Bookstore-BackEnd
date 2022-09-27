using AutoMapper;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.ShoppingCarts.Commands
{
    public class DeleteBookFromShoppingCartCommandHandler : IRequestHandler<DeleteBookFromShoppingCartCommand, ShoppingCart>
    {
        private readonly IMapper _mapper;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public DeleteBookFromShoppingCartCommandHandler(IMapper mapper, IShoppingCartRepository shoppingCartRepository)
        {
            _mapper = mapper;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<ShoppingCart> Handle(DeleteBookFromShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _shoppingCartRepository.GetAsync(request.ShoppingCartItem.UserId, request.ShoppingCartItem.BookId);
            if (shoppingCart == null)
            {
                return null;
            }

            ShoppingCart updatedCart = new ShoppingCart();
            
            if (shoppingCart.Quantity == 1)
            {
                updatedCart = await _shoppingCartRepository.DeleteAsync(shoppingCart.Id);
                await _shoppingCartRepository.SaveChangesAsync();
            }

            if (shoppingCart.Quantity > 1)
            {
                shoppingCart.Quantity--;
                updatedCart = await _shoppingCartRepository.UpdateAsync(shoppingCart);
            }
            
            return updatedCart;
        }
    }
}
