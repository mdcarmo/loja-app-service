using Microsoft.EntityFrameworkCore;

namespace LojaAppApi.Model
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext>
           options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>().HasKey(m => m.CustomerID);
            builder.Entity<Item>().HasKey(m => m.ItemID);
            builder.Entity<Order>().HasKey(m => m.OrderID);
            builder.Entity<OrderItem>().HasKey(m => m.OrderItemID);
            base.OnModelCreating(builder);
        }
    }
}
