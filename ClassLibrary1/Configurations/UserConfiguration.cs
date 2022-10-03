using ITPLibrary.Application.Validation.ValidationConstants;
using ITPLibrary.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITPLibrary.Infrastructure.Persistance.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(UserValidationRules.NameMaxLength);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(UserValidationRules.EmailMaxLength);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(UserValidationRules.NameMaxLength);

           /* builder.HasMany(x => x.Orders)
                .WithOne(o => o.User);

            builder.HasMany(x => x.ShoppingCartItems)
                .WithOne(s => s.User);

            builder.HasMany(x => x.RecoveryCodes)
                .WithOne(r => r.User);*/
        }
    }
}
