using ITPLibrary.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITPLibrary.Infrastructure.Persistance.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .IsRequired()
                .UseIdentityColumn();

            builder.Property(o => o.Price)
                .IsRequired(true);

            builder.Property(o => o.Quantity)
                .IsRequired(true);

            builder.HasOne(o => o.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(o => o.OrderId);

            builder.HasOne(o => o.Book)
                .WithMany(b => b.OrderItems)
                .HasForeignKey(o => o.BookId);
        }
    }
}
