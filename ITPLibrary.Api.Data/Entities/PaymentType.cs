using System.ComponentModel.DataAnnotations.Schema;

namespace ITPLibrary.Api.Data.Entities
{
    public class PaymentType
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
