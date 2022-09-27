using AutoMapper;
using ITPLibrary.Application.Features.Addresses.ViewModels;
using ITPLibrary.Application.Features.Books.ViewModels;
using ITPLibrary.Application.Features.BooksDetails.ViewModels;
using ITPLibrary.Application.Features.ShoppingCarts.ViewModels;
using ITPLibrary.Application.Features.Users.ViewModels;
using ITPLibrary.Application.Profiles.MappingProfilesMethods;
using ITPLibrary.Domain.Entites;

namespace ITPLibrary.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Address
            CreateMap<AddressVm, Address>().ReverseMap();
            #endregion

            #region Book
            CreateMap<Book, RecentlyAddedAndPopularBookVm>().
                ForMember(dest => dest.RecentlyAdded,
                            opt => opt.MapFrom
                            (src => MappingMethods.IsBookRecentlyAdded(src.AddedDateTime)));
            
            CreateMap<BookPostVm, Book>();

            CreateMap<Book, PopularBookVm>();

            CreateMap<Book, BookWithDetailsVm>();
            #endregion

            #region BookDetails
            CreateMap<BookDetails, BookDetailsVm>();
            #endregion

            #region Order

            #endregion

            #region OrderItem

            #endregion

            #region OrderStatus

            #endregion

            #region PaymentType

            #endregion

            #region RecoveryCode

            #endregion

            #region ShoppingCart
            CreateMap<ShoppingCartItemVm, ShoppingCart>();
            CreateMap<ShoppingCart, DisplayShoppingCartVm>();
            #endregion

            #region User
            CreateMap<RegisterUserVm, User>();
            #endregion
        }
    }
}
