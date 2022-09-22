using DocumentFormat.OpenXml.Wordprocessing;
using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.Enums;
using ITPLibrary.Api.Data.Entities.ValidationRules;
using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Api.Core.Dtos
{
    public class OrderPostDto
    {
        [Required]
        public AddressDto BillingAddress { get; set; }

        [Required]
        public AddressDto DeliveryAddress { get; set; }

        [Required]
        public PaymentTypeEnum PaymentType { get; set; }

        public DateTimeOffset DeliveryDate { get; set; }

        [MaxLength(OrderValidationRules.ObservationsMaxLength)]
        public string Observations { get; set; }

        public bool RecommendUs { get; set; }
    }
}
