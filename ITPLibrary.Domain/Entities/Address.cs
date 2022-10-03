using System.ComponentModel.DataAnnotations.Schema;

namespace ITPLibrary.Domain.Entites
{
    public class Address
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public string AddressLine { get; set; }

        public string PhoneNumber { get; set; }
        [NotMapped]
        public List<Order> Orders { get; set; }
    }
}
