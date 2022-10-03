using AutoMapper;
using ITPLibrary.Api.Data.Entities.Enums;
using ITPLibrary.Application.Features.Addresses.ViewModels;
using ITPLibrary.Application.Features.Books.ViewModels;
using ITPLibrary.Application.Features.BooksDetails.ViewModels;
using ITPLibrary.Application.Features.Orders.ViewModels;
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
            CreateMap<OrderPostVm, Order>()
                .ForMember(dest => dest.PaymentTypeId,
                            opt => opt.MapFrom
                                (src => (int)src.PaymentType))
                .ForMember(dest=>dest.PaymentType,
                            opt=>opt.Ignore());
            
            CreateMap<ShoppingCart, OrderItem>()
                .ForMember(dest => dest.Quantity,
                            opt => opt.MapFrom
                                (src => src.Quantity))
                .ForMember(dest => dest.Price,
                            opt => opt.MapFrom
                                (src => src.Book.Price))
                .ForMember(dest => dest.BookId,
                            opt => opt.MapFrom
                                (src => src.BookId))
                .ForMember(dest => dest.Id,
                            opt => opt.Ignore());

            CreateMap<Order, OrderDisplayVm>()
                .ForMember(dest => dest.Status,
                            opt => opt.MapFrom
                                (src => src.OrderStatusId))
                .ForMember(dest => dest.NumberOfItems,
                            opt => opt.MapFrom
                                (src => MappingMethods.CalculateNumberOfItems(src.Items)));
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
            CreateMap<User, SucessfulUserLoginVm>();
            #endregion
        }
    }
}
