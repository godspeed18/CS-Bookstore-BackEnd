using AutoMapper;
using ITPLibrary.Api.Core.Services.Interfaces;
using ITPLibrary.Api.Data.Repositories.Interfaces;

namespace ITPLibrary.Api.Core.Services.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _repository;
        private readonly IMapper _mapper;

        public ShoppingCartService(IShoppingCartRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
    }
}
