using ITPLibrary.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITPLibrary.Infrastructure.Persistance.Configurations
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .IsRequired()
                .UseIdentityColumn();

            builder.Property(s => s.Quantity)
                .IsRequired();

            builder.HasOne(s => s.User)
                .WithMany(u => u.ShoppingCartItems)
                .HasForeignKey(s => s.UserId);

            builder.HasOne(s => s.Book)
                .WithMany(b => b.ShoppingCartItems)
                .HasForeignKey(s => s.BookId);
        }
    }
}
