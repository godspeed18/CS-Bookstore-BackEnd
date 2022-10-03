using ITPLibrary.Application.Validation.ValidationConstants;
using ITPLibrary.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITPLibrary.Infrastructure.Persistance.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .IsRequired()
                .UseIdentityColumn();

            builder.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(AddressValidationRules.CountryMaxLength);

            builder.Property(a => a.AddressLine)
                .IsRequired()
                .HasMaxLength(AddressValidationRules.AddressLineMaxLength);

            builder.Property(a => a.PhoneNumber)
                .IsRequired()
                .HasMaxLength(AddressValidationRules.PhoneNumberMinLength);

           /* builder.HasMany(a => a.Orders)
           .WithOne(o => o.BillingAddress)
           .HasForeignKey(o => o.BillingAddressId);

            builder.HasMany(a => a.Orders)
                .WithOne(o => o.DeliveryAddress)
                .HasForeignKey(o => o.DeliveryAddressId);*/
        }
    }
}
