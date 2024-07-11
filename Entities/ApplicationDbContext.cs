using Microsoft.EntityFrameworkCore;
using ECommWeb.Models;
namespace ECommWeb.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Vendor> Vendor { get; set; }

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }

        public DbSet<Inventory> Inventory { get; set; }

        public DbSet<Shipping> Shipping { get; set; }
        public DbSet<Profile> Profile { get; set; }

        public DbSet<Discount> Discount { get; set; }

        public DbSet<Payment> Payment { get; set; }
        public DbSet<Return> Return { get; set; }
        public DbSet<Review> Review { get; set; }

        public DbSet<Category> Category { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderID);
            modelBuilder.Entity<Order>()
                 .HasOne(o => o.Shipping)
                 .WithOne(s => s.Order)
                 .HasForeignKey<Shipping>(s => s.OrderID);




            base.OnModelCreating(modelBuilder);
        }

    }
}
