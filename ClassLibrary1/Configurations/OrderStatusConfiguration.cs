using ITPLibrary.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITPLibrary.Infrastructure.Persistance.Configurations
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .IsRequired()
                .UseIdentityColumn();
        }
    }
}
