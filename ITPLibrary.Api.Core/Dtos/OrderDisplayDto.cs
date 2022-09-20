using ITPLibrary.Api.Data.Entities.Enums;

namespace ITPLibrary.Api.Core.Dtos
{
    public class OrderDisplayDto
    {
        public int Id { get; set; }
        public int NumberOfItems { get; set; }
        public int TotalPrice { get; set; }
        public OrderStatusEnum Status { get; set; }
    }
}
