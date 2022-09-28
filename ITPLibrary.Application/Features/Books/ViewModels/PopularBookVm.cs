using ITPLibrary.Application.Features.BooksDetails.ViewModels;

namespace ITPLibrary.Application.Features.Books.ViewModels
{
    public class PopularBookVm
    {
        public string Title { get; set; }
        public byte[] Thumbnail { get; set; }
        public BookDetailsVm BookDetails { get; set; }
    }
}
