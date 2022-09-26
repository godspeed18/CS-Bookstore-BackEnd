using ITPLibrary.Application.Features.Addresses.ViewModels;
using MediatR;

namespace ITPLibrary.Application.Features.Addresses.Queries
{
    public class GetAddressByIdQuery : IRequest<AddressVm>
    {
        public int Id { get; set; }
    }
}
