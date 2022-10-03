using ITPLibrary.Application.Validation.ValidationConstants;
using ITPLibrary.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITPLibrary.Infrastructure.Persistance.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.UserId)
                .IsRequired();

            builder.Property(o => o.DeliveryDate)
                .IsRequired();

            builder.Property(o => o.Observations)
                .IsRequired()
                .HasMaxLength(OrderValidationRules.ObservationsMaxLength);

            builder.Property(o => o.PaymentType)
                .IsRequired();

            builder.Property(o => o.OrderStatus)
                .IsRequired();

            builder.Property(o => o.DeliveryAddress)
                .IsRequired();

            builder.Property(o => o.BillingAddress)
                .IsRequired();

            builder.Property(o => o.User)
                .IsRequired();

           /* builder.HasOne(o => o.User)
                 .WithMany(u=>u.Orders)
                 .HasForeignKey(o => o.UserId);

            builder.HasOne(o => o.BillingAddress)
                .WithMany(a => a.Orders)
                .HasForeignKey(o => o.BillingAddressId);

            builder.HasOne(o => o.DeliveryAddress)
                .WithMany(a => a.Orders)
                .HasForeignKey(o => o.DeliveryAddressId);

            builder.HasOne(o => o.PaymentType)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.PaymentTypeId);

            builder.HasOne(o => o.OrderStatus)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.OrderStatusId);

            builder.HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId);
           */
        }
    }
}
