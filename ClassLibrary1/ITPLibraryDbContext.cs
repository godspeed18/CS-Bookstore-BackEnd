using ITPLibrary.Api.Data.Entities.Enums;
using ITPLibrary.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace ITPLibrary.Infrastructure.Persistance
{
    public class ITPLibraryDbContext : DbContext
    {
        public ITPLibraryDbContext(DbContextOptions<ITPLibraryDbContext> options) : base(options)
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
        public DbSet<OrderItem> OrderItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.ApplyConfigurationsFromAssembly(typeof(ITPLibraryDbContext).Assembly);*/

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

