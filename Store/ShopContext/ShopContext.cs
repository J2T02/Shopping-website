using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Store.Entity;

namespace Store.Database
{
    public class ShopContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
                .HasOne(e => e.Customer)
 .WithMany(e => e.Orders)
 .HasForeignKey(e => e.CustomerId)
 .HasConstraintName("FK_Order_Customer")
 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
