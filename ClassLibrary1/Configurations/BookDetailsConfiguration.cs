using ITPLibrary.Application.Validation.ValidationConstants;
using ITPLibrary.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITPLibrary.Infrastructure.Persistance.Configurations
{
    public class BookDetailsConfiguration : IEntityTypeConfiguration<BookDetails>
    {
        public void Configure(EntityTypeBuilder<BookDetails> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id)
                .IsRequired()
                .UseIdentityColumn();

            builder.Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(BookDetailsValidationRules.DescriptionMaxLength);

            builder.HasOne(b => b.Book)
                .WithOne(b => b.BookDetails)
                .HasForeignKey<BookDetails>(b => b.BookId);
        }
    }
}
