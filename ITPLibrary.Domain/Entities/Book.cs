namespace ITPLibrary.Domain.Entites
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int Price { get; set; }

        public byte[]? Thumbnail { get; set; }

        public DateTimeOffset AddedDateTime { get; set; }
        
        public bool Popular { get; set; }

        public BookDetails BookDetails { get; set; } 
    }
}
