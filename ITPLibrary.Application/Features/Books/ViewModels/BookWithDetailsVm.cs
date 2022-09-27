using ITPLibrary.Application.Features.BooksDetails.ViewModels;

namespace ITPLibrary.Application.Features.Books.ViewModels
{
    public class BookWithDetailsVm
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public int Price { get; set; }

        public byte[]? Thumbnail { get; set; }

        public BookDetailsVm BookDetails { get; set; }
    }
}
