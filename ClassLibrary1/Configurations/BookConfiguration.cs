using ITPLibrary.Application.Validation.ValidationConstants;
using ITPLibrary.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITPLibrary.Infrastructure.Persistance.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id)
                .IsRequired()
                .UseIdentityColumn();

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(BookValidationRules.TitleMaxLength);

            builder.Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(BookValidationRules.AuthorMaxLength);

            builder.Property(b => b.Popular)
                .IsRequired();

            builder.Property(b => b.Price)
                .IsRequired();

            builder.Property(b => b.AddedDateTime)
                .IsRequired();

            builder.HasOne(b => b.BookDetails)
                .WithOne(b => b.Book);

            builder.HasMany(b => b.OrderItems)
                .WithOne(o => o.Book)
                .HasForeignKey(o => o.BookId);

            builder.HasMany(b => b.ShoppingCartItems)
                .WithOne(s => s.Book)
                .HasForeignKey(s => s.BookId);
        }
    }
}
