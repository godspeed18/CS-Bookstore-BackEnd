using AutoMapper;
using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.Validation_Rules;

namespace ITPLibrary.Api.Core.Profiles
{
    public class BookDtoProfile : Profile
    {
        public BookDtoProfile()
        {
            CreateMap<BookDto, Book>();

            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.RecentlyAdded,
                            opt => opt.MapFrom
                                (src => BookValidationRules.
                                    IsBookRecentlyAdded(src.AddedDateTime)));

            CreateMap<PostBookDto, Book>();

            CreateMap<Book, PromotedBookDto>()
                .ForMember(dest => dest.Thumbnail,
                            opt => opt.MapFrom
                                (src => ImageConverter.ByteArrayToImage(src.Thumbnail)))
                .ForMember(dest => dest.Description,
                            opt => opt.MapFrom
                                (src => src.BookDetails.Description));

            CreateMap<Book, RecentlyAddedAndPopularBookDto>()
                .ForMember(dest => dest.Thumbnail,
                            opt => opt.MapFrom
                                (src => ImageConverter.ByteArrayToImage(src.Thumbnail)))
                .ForMember(dest => dest.RecentlyAdded,
                            opt => opt.MapFrom
                                (src => BookValidationRules.
                                    IsBookRecentlyAdded(src.AddedDateTime)));

            CreateMap<Book, BookDetailsDto>()
                .ForMember(dest => dest.Thumbnail,
                            opt => opt.MapFrom
                                (src => ImageConverter.ByteArrayToImage(src.Thumbnail)));

            CreateMap<Book, BookDisplayDto>();
        }
    }
}
