using ITPLibrary.Api.Data.Entities.Validation_Rules;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ITPLibrary.Api.Core.Dtos
{
    public class RecentlyAddedAndPopularBookDto
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

        public Image Thumbnail { get; set; }

        [Required]
        public bool RecentlyAdded { get; set; }

        [Required]
        public bool Popular { get; set; }
    }
}