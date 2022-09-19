using System.ComponentModel.DataAnnotations.Schema;

namespace ITPLibrary.Api.Data.Entities
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
