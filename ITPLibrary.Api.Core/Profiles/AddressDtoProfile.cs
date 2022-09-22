using AutoMapper;
using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Data.Entities;

namespace ITPLibrary.Api.Core.Profiles
{
    public class AddressDtoProfile : Profile
    {
        public AddressDtoProfile()
        {
            CreateMap<AddressDto, Address>()
                .ForMember(dest=>dest.Id,
                        opt=>opt.Ignore());
        }
    }
}
