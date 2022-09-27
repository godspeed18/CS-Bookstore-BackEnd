namespace ITPLibrary.Domain.Entites
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BillingAddressId { get; set; }

        public int DeliveryAddressId { get; set; }

        public int OrderStatusId { get; set; }

        public int PaymentTypeId { get; set; }

        public DateTimeOffset DeliveryDate { get; set; }

        public string Observations { get; set; }

        public Address BillingAddress { get; set; }

        public Address DeliveryAddress { get; set; }

        public User User { get; set; }

        public int TotalPrice { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
