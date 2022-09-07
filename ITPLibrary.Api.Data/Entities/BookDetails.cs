using System.ComponentModel.DataAnnotations.Schema;

namespace ITPLibrary.Api.Data.Entities
{
    public class BookDetails
    {
        public int Id { get; set; }
       
        public string Description { get; set; }

        [ForeignKey("FK_BookDetails_BookId")]
        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}
