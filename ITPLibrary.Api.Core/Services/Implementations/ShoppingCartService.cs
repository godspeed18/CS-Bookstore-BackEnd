using AutoMapper;
using ITPLibrary.Api.Core.Services.Interfaces;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Repositories.Interfaces;

namespace ITPLibrary.Api.Core.Services.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IBookRepository bookRepository, IMapper mapper)
        {
            _mapper = mapper;
            _shoppingCartRepository = shoppingCartRepository;
            _bookRepository = bookRepository;
        }

        public async Task<bool> PostBookInCart(int userId, int bookId)
        {
            if (await _bookRepository.GetBookById(bookId) == null)
            {
                return false;
            }

            var bookInCart = await _shoppingCartRepository.GetBookFromCart(userId, bookId);

            if (bookInCart == null)
            {
                ShoppingCart newBookInCart = new ShoppingCart()
                {
                    BookId = bookId,
                    UserId = userId,
                    Quantity = 1
                };

                await _shoppingCartRepository.PostNewBookInCart(newBookInCart);
            }
            else
            {
                var incrementQuantity = await _shoppingCartRepository.IncrementBookQuantityInCart(userId, bookId);
                return incrementQuantity;
            }

            return true;
        }
    }
}
