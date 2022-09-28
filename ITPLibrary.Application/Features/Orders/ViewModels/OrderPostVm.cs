using ITPLibrary.Api.Data.Entities.Enums;
using ITPLibrary.Application.Features.Addresses.ViewModels;
using ITPLibrary.Application.Validation.ValidationConstants;
using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Application.Features.Orders.ViewModels
{
    internal class OrderPostVm
    {
        [Required]
        public AddressVm BillingAddress { get; set; }

        [Required]
        public AddressVm DeliveryAddress { get; set; }

        [Required]
        public PaymentTypeEnum PaymentType { get; set; }

        public DateTimeOffset DeliveryDate { get; set; }

        [MaxLength(OrderValidationRules.ObservationsMaxLength)]
        public string Observations { get; set; }

        public bool RecommendUs { get; set; }
    }
}
