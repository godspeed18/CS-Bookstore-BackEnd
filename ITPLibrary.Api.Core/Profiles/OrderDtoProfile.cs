using AutoMapper;
using ITPLibrary.Api.Core.Dtos;
using ITPLibrary.Api.Data.Entities;
using Methods;

namespace ITPLibrary.Api.Core.Profiles
{
    public class OrderDtoProfile : Profile
    {
        public OrderDtoProfile()
        {
            CreateMap<OrderPostDto, Order>()
                .ForMember(dest => dest.PaymentTypeId,
                            opt => opt.MapFrom
                                (src => (int)src.PaymentType));

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

            CreateMap<Order, OrderDisplayDto>()
                .ForMember(dest => dest.Status,
                            opt => opt.MapFrom
                                (src => src.OrderStatusId))
                .ForMember(dest => dest.NumberOfItems,
                            opt => opt.MapFrom
                                (src => CalculateNumberOfItems(src.Items)));
        }

        public static int CalculateNumberOfItems(IEnumerable<OrderItem> items)
        {
            int total = 0;
            foreach (var item in items)
            {
                total += item.Quantity;
            }

            return total;
        }

    }
}
