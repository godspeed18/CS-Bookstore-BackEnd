using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.Validation_Rules;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
              new Book { Id = 1, Title = "Why Am I So Clever", Author = "Friederich Nietzsche", Price = 50 },
              new Book { Id = 2, Title = "Percy Jackson And The Olympians", Author = "Rick Riordan", Price = 35 },
              new Book { Id = 3, Title = "Wonder", Author = "R. J. Palacio", Price = 42 }
              );

            modelBuilder.Entity<Book>().Property(p => p.Author).HasMaxLength(BookValidationRules.AuthorMaxLength);
            modelBuilder.Entity<Book>().Property(p => p.Title).HasMaxLength(BookValidationRules.TitleMaxLength);

            modelBuilder.Entity<User>().Property(p => p.Name).HasMaxLength(UserValidationRules.NameMaxLength);
            modelBuilder.Entity<User>().Property(p => p.Email).HasMaxLength(UserValidationRules.EmailMaxLength);
            modelBuilder.Entity<User>().Property(p => p.Password).HasMaxLength(UserValidationRules.PasswordMaxLength);

            base.OnModelCreating(modelBuilder);
        }
    }
}
