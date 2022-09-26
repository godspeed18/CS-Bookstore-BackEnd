using AutoMapper;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Application.Features.Addresses.ViewModels;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.Addresses.Queries
{
    public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, AddressVm>
    {
        private readonly IAsyncRepository<Address> _addressRepository;
        private readonly IMapper _mapper;

        public GetAddressByIdQueryHandler(IAsyncRepository<Address> addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<AddressVm> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _addressRepository.GetByIdAsync(request.Id);
            return _mapper.Map<AddressVm>(response);
        }
    }
}
