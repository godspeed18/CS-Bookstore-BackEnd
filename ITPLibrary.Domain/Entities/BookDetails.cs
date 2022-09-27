namespace ITPLibrary.Domain.Entites
{
    public class BookDetails
    {
        public int Id { get; set; }
       
        public string Description { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}
