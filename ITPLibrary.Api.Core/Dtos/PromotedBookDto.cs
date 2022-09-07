using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.Validation_Rules;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace ITPLibrary.Api.Core.Dtos
{
    public class PromotedBookDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(BookValidationRules.TitleMaxLength)]
        public string Title { get; set; }

        public Image Thumbnail { get; set; }

        [MaxLength(BookDetailsValidationRules.DescriptionMaxLength)]
        public string Description { get; set; }
    }
}
