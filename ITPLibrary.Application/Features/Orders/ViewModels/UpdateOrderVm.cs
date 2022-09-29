using ITPLibrary.Api.Data.Entities.Enums;
using ITPLibrary.Application.Features.Addresses.ViewModels;
using ITPLibrary.Application.Validation.ValidationConstants;
using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Application.Features.Orders.ViewModels
{
    public class UpdateOrderVm
    {
        [Required]
        public int Id { get; set; }

        public AddressVm BillingAddress { get; set; }

        public AddressVm DeliveryAddress { get; set; }

        [MaxLength(OrderValidationRules.ObservationsMaxLength)]
        public string Observations { get; set; }

        public PaymentTypeEnum PaymentType { get; set; }
    }
}
