using AutoMapper;
using Common;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ITPLibrary.Application.Features.ShoppingCarts.Commands
{
    public class DeleteBookFromShoppingCartCommandHandler : IRequestHandler<DeleteBookFromShoppingCartCommand, ShoppingCart>
    {
        private readonly IMapper _mapper;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public DeleteBookFromShoppingCartCommandHandler
            (
            IMapper mapper,
                IShoppingCartRepository shoppingCartRepository
            )
        {
            _mapper = mapper;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<ShoppingCart> Handle(DeleteBookFromShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _shoppingCartRepository.GetAsync(request.UserId, request.BookId);

            if (shoppingCart == null)
            {
                return null;
            }

            ShoppingCart updatedCart = shoppingCart;

            if (shoppingCart.Quantity == 1)
            {
                await _shoppingCartRepository.DeleteAsync(shoppingCart);
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
