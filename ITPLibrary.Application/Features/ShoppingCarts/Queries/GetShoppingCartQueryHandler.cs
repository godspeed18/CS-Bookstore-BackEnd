using AutoMapper;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Application.Features.ShoppingCarts.ViewModels;
using ITPLibrary.Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ITPLibrary.Application.Features.ShoppingCarts.Queries
{
    public class GetShoppingCartQueryHandler : IRequestHandler<GetShoppingCartQuery, IReadOnlyList<DisplayShoppingCartVm>>
    {
        private readonly IAsyncRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetShoppingCartQueryHandler
            (
            IAsyncRepository<ShoppingCart> shoppingCartRepository, 
                IHttpContextAccessor httpContextAccessor,
                    IMapper mapper
            )
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
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
