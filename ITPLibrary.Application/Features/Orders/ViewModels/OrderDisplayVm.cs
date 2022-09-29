using ITPLibrary.Api.Data.Entities.Enums;

namespace ITPLibrary.Application.Features.Orders.ViewModels
{
    public class OrderDisplayVm
    {
        public int Id { get; set; }
        public int NumberOfItems { get; set; }
        public int TotalPrice { get; set; }
        public OrderStatusEnum Status { get; set; }
    }
}
