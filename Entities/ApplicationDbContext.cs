using Microsoft.EntityFrameworkCore;
using ECommWeb.Models;
namespace ECommWeb.Entities
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Vendor> Vendor { get; set; }

        //public DbSet<Role> Roles { get; set; }

        public DbSet<Category> Category { get; set; }
    }
}
