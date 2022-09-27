using AutoMapper;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.ShoppingCarts.Commands
{
    public class AddBookToShoppingCartCommandHandler : IRequestHandler<AddBookToShoppingCartCommand, ShoppingCart>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;

        public AddBookToShoppingCartCommandHandler(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
        }

        public async Task<ShoppingCart> Handle(AddBookToShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _shoppingCartRepository.GetAsync(request.ShoppingCartItem.UserId, request.ShoppingCartItem.BookId);
            if (shoppingCart == null)
            {
                ShoppingCart shoppingCartItem = _mapper.Map<ShoppingCart>(request.ShoppingCartItem);
                shoppingCartItem.Quantity = 0;

                return await _shoppingCartRepository.AddAsync(shoppingCartItem);
            }

            shoppingCart.Quantity++;

            var updatedCart = await _shoppingCartRepository.UpdateAsync(shoppingCart);
            await _shoppingCartRepository.SaveChangesAsync();

            return updatedCart;
        }
    }
}
