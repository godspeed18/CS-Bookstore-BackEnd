

using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.Enums;
using ITPLibrary.Api.Data.Entities.ValidationRules;
using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Api.Core.Dtos
{
    public class UpdateOrderDto
    {
        [Required]
        public int Id { get; set; }

        public Address BillingAddress { get; set; }

        public Address DeliveryAddress { get; set; }

        [MaxLength(OrderValidationRules.ObservationsMaxLength)]
        public string Observations { get; set; }

        public PaymentTypeEnum PaymentType { get; set; }
    }
}
