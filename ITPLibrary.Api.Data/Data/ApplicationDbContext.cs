using ITPLibrary.Api.Data.Entities;
using ITPLibrary.Api.Data.Entities.Enums;
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
        public DbSet<Order> Orders { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Property(p => p.Observations).HasMaxLength(OrderValidationRules.ObservationsMaxLength);

            modelBuilder.Entity<Address>().Property(p => p.AddressLine).HasMaxLength(AddressValidationRules.AddressLineMaxLength);
            modelBuilder.Entity<Address>().Property(p => p.Country).HasMaxLength(AddressValidationRules.CountryMaxLength);
            modelBuilder.Entity<Address>().Property(p => p.PhoneNumber).HasMaxLength(AddressValidationRules.PhoneNumberMaxLength);

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

            modelBuilder.Entity<OrderStatus>().HasData(
                    new OrderStatus
                    {
                        Id = (int)OrderStatusEnum.New,
                        Status = OrderStatusEnum.New.ToString()
                    }
                    );
            modelBuilder.Entity<OrderStatus>().HasData(
                    new OrderStatus
                    {
                        Id = (int)OrderStatusEnum.Processing,
                        Status = OrderStatusEnum.Processing.ToString()
                    }
                    );
            modelBuilder.Entity<OrderStatus>().HasData(
                    new OrderStatus
                    {
                        Id = (int)OrderStatusEnum.Dispatched,
                        Status = OrderStatusEnum.Dispatched.ToString()
                    }
                    );
            modelBuilder.Entity<OrderStatus>().HasData(
                    new OrderStatus
                    {
                        Id = (int)OrderStatusEnum.Closed,
                        Status = OrderStatusEnum.Closed.ToString()
                    }
                    );

            modelBuilder.Entity<PaymentType>().HasData(
                   new PaymentType
                   {
                       Id = (int)PaymentTypeEnum.Cash,
                       Type = PaymentTypeEnum.Cash.ToString()
                   }
                   );
            modelBuilder.Entity<PaymentType>().HasData(
                   new PaymentType
                   {
                       Id = (int)PaymentTypeEnum.CreditCard,
                       Type = PaymentTypeEnum.CreditCard.ToString()
                   }
                   );

            base.OnModelCreating(modelBuilder);
        }
    }
}
