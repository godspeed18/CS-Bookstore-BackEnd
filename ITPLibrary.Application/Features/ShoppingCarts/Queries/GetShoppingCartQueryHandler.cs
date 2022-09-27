using AutoMapper;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Application.Features.ShoppingCarts.ViewModels;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.ShoppingCarts.Queries
{
    public class GetShoppingCartQueryHandler : IRequestHandler<GetShoppingCartQuery, IReadOnlyList<DisplayShoppingCartVm>>
    {
        private readonly IAsyncRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IMapper _mapper;

        public GetShoppingCartQueryHandler(IAsyncRepository<ShoppingCart> shoppingCartRepository, IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<DisplayShoppingCartVm>> Handle(GetShoppingCartQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _shoppingCartRepository.GetAllAsync();
            if (shoppingCart == null)
            {
                return null;
            }

            var displayShoppingCart = _mapper.Map<IReadOnlyList<DisplayShoppingCartVm>>(shoppingCart);
            return displayShoppingCart;
        }
    }
}
