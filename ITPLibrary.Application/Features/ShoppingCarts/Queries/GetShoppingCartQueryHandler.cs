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
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;

        public GetShoppingCartQueryHandler
            (
           IShoppingCartRepository shoppingCartRepository,
                    IMapper mapper
            )
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<DisplayShoppingCartVm>> Handle(GetShoppingCartQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _shoppingCartRepository.GetUserShoppingCart(request.UserId);
            if (shoppingCart == null)
            {
                return null;
            }

            var displayShoppingCart = _mapper.Map<IReadOnlyList<DisplayShoppingCartVm>>(shoppingCart);
            return displayShoppingCart;
        }
    }
}
