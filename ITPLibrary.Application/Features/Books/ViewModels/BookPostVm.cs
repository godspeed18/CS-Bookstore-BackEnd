using ITPLibrary.Application.Features.BooksDetails.ViewModels;
using ITPLibrary.Application.Validation.ValidationConstants;
using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Application.Features.Books.ViewModels
{
    public class BookPostVm
    {
        [Required]
        [MaxLength(BookValidationRules.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(BookValidationRules.AuthorMaxLength)]
        public string Author { get; set; }

        [Range(BookValidationRules.PriceMin, BookValidationRules.PriceMax)]
        public int Price { get; set; }

        public byte[]? Thumbnail { get; set; }

        [Required]
        public DateTimeOffset RecentlyAdded { get; set; }

        [Required]
        public bool Popular { get; set; }

        public BookWithDetailsVm BookDetails { get; set; }
    }
}
