using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.Validation_Rules;
using ITPLibrary.Api.Data.Entities.ValidationRules;
using Microsoft.EntityFrameworkCore;

namespace ITPLibrary.Api.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BookDetails> BookDetails { get; set; }
        public DbSet<RecoveryCode> RecoveryCodes { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(p => p.Author).HasMaxLength(BookValidationRules.AuthorMaxLength);
            modelBuilder.Entity<Book>().Property(p => p.Title).HasMaxLength(BookValidationRules.TitleMaxLength);
            modelBuilder.Entity<Book>().HasKey(p => p.Id);

            modelBuilder.Entity<User>().HasKey(p => p.Id);
            modelBuilder.Entity<User>().Property(p => p.Name).HasMaxLength(UserValidationRules.NameMaxLength);
            modelBuilder.Entity<User>().Property(p => p.Email).HasMaxLength(UserValidationRules.EmailMaxLength);
            modelBuilder.Entity<User>().Property(p => p.HashedPassword).HasMaxLength(UserValidationRules.PasswordMaxLength);
            modelBuilder.Entity<User>().Property(p => p.Salt).HasMaxLength(UserValidationRules.SaltMaxLength);

            modelBuilder.Entity<BookDetails>().Property(p => p.Description).HasMaxLength(BookDetailsValidationRules.DescriptionMaxLength);

            modelBuilder.Entity<RecoveryCode>().Property(p => p.Code).HasMaxLength(RecoveryCodeValidationRules.RecoveryCodeMaxLength);

            base.OnModelCreating(modelBuilder);
        }
    }
}
