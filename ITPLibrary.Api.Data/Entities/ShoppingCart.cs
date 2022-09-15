using System.ComponentModel.DataAnnotations.Schema;

namespace ITPLibrary.Api.Data.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("FK_Book_ShoppingCart")]
        public int BookId { get; set; }

        [ForeignKey("FK_User_ShoppingCart")]
        public int UserId { get; set; }

        public Book Book { get; set; }

        public User User { get; set; }
    }
}
