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
        private readonly IBookRepository _bookRepository;

        public AddBookToShoppingCartCommandHandler
            (
             IShoppingCartRepository shoppingCartRepository,
                IBookRepository bookRepository,
                    IMapper mapper
            )
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        public async Task<ShoppingCart> Handle(AddBookToShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _shoppingCartRepository.GetAsync(request.UserId, request.BookId);
            var book = await _bookRepository.GetByIdAsync(request.BookId);

            if(book==null)
            {
                return null;
            }

            if (shoppingCart == null)
            {
                ShoppingCart shoppingCartItem = new ShoppingCart();
                shoppingCartItem.BookId = request.BookId;
                shoppingCartItem.Quantity = 1;
                shoppingCartItem.UserId = request.UserId;

                return await _shoppingCartRepository.AddAsync(shoppingCartItem);
            }

            shoppingCart.Quantity++;

            var updatedCart = await _shoppingCartRepository.UpdateAsync(shoppingCart);

            return updatedCart;
        }
    }
}
