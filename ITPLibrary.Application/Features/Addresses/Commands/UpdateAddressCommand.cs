using ITPLibrary.Application.Features.Addresses.ViewModels;
using ITPLibrary.Domain.Entites;
using MediatR;

namespace ITPLibrary.Application.Features.Addresses.Commands
{
    public class UpdateAddressCommand : IRequest<Address>
    {
        public int Id { get; set; }
        public AddressVm UpdatedAddress { get; set; }
    }
}