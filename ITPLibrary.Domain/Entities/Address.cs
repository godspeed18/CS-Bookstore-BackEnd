namespace ITPLibrary.Domain.Entites
{
    public class Address
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public string AddressLine { get; set; }

        public string PhoneNumber { get; set; }
        public List<Order> Orders { get; set; }
    }
}
