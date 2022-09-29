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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteBookFromShoppingCartCommandHandler
            (
            IMapper mapper, 
                IShoppingCartRepository shoppingCartRepository,
                    IHttpContextAccessor httpContextAccessor
            )
        {
            _mapper = mapper;
            _shoppingCartRepository = shoppingCartRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ShoppingCart> Handle(DeleteBookFromShoppingCartCommand request, CancellationToken cancellationToken)
        {
            int userId = CommonMethods.GetUserIdFromContext(_httpContextAccessor.HttpContext);

            var shoppingCart = await _shoppingCartRepository.GetAsync(userId, request.BookId);

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
