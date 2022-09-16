using ITPLibrary.Api.Data.Entities;

namespace ITPLibrary.Api.Core.Dtos
{
    public class BookDisplayDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int Price { get; set; }

        public BookDetails BookDetails { get; set; }

        public int Quantity { get; set; }
    }
}
