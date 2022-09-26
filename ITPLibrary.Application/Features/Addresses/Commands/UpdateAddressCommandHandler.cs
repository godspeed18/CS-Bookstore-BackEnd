using AutoMapper;
using ITPLibrary.Application.Contracts.Persistance;
using ITPLibrary.Application.Features.Addresses.ViewModels;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.Addresses.Commands
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, Address>
    {
        private readonly IAsyncRepository<Address> _addressRepository;
       
        public UpdateAddressCommandHandler(IAsyncRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Address> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var addressToBeUpdated = await _addressRepository.GetByIdAsync(request.Id);
            if (addressToBeUpdated == null)
            {
                return null;
            }

            addressToBeUpdated.AddressLine = request.UpdatedAddress.AddressLine;
            addressToBeUpdated.PhoneNumber = request.UpdatedAddress.PhoneNumber;
            addressToBeUpdated.Country = request.UpdatedAddress.Country;

            var response = await _addressRepository.UpdateAsync(addressToBeUpdated);
            await _addressRepository.SaveChangesAsync();
            return response;
        }
    }
}
