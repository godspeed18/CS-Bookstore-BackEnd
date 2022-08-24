using AutoMapper;
using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Data.Entities;

namespace ITPLibrary.Api.Core.Profiles
{
    public class BookDtoProfile : Profile
    {
        public BookDtoProfile()
        {
            CreateMap<Book, BookDto>();
        }
    }
}
