namespace ITPLibrary.Domain.Entites
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public List<Order> Orders { get; set; }
    }
}
