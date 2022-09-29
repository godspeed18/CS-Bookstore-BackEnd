using AutoMapper;
using Common;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ITPLibrary.Application.Features.ShoppingCarts.Commands
{
    public class AddBookToShoppingCartCommandHandler : IRequestHandler<AddBookToShoppingCartCommand, ShoppingCart>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddBookToShoppingCartCommandHandler
            (
             IShoppingCartRepository shoppingCartRepository,
                IMapper mapper,
                    IHttpContextAccessor httpContextAccessor
            )
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ShoppingCart> Handle(AddBookToShoppingCartCommand request, CancellationToken cancellationToken)
        {
            int userId = CommonMethods.GetUserIdFromContext(_httpContextAccessor.HttpContext);

            var shoppingCart = await _shoppingCartRepository.GetAsync(userId, request.BookId);
            
            if (shoppingCart == null)
            {
                ShoppingCart shoppingCartItem = new ShoppingCart();
                shoppingCartItem.BookId = request.BookId;
                shoppingCartItem.Quantity = 1;
                shoppingCartItem.UserId = userId;

                return await _shoppingCartRepository.AddAsync(shoppingCartItem);
            }

            shoppingCart.Quantity++;

            var updatedCart = await _shoppingCartRepository.UpdateAsync(shoppingCart);
            await _shoppingCartRepository.SaveChangesAsync();

            return updatedCart;
        }
    }
}
