using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.Validation_Rules;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITPLibrary.Api.Core.Dtos
{
    public class PostBookDto
    {
        [Key]
        public int Id { get; set; }

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

        public BookDetails BookDetails { get; set; }
    }
}
