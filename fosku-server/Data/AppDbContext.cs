using fosku_server.Models;
using Microsoft.EntityFrameworkCore;

namespace fosku_server.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Manager> Managers { get; set; }

        //TODO: remove this (for now)
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Product>()
        //         .HasMany(p => p.ProductImages)
        //         .WithOne(pi => pi.Product) 
        //         .HasForeignKey(pi => pi.ProductId) 
        //         .OnDelete(DeleteBehavior.Cascade);
        // }
    }
}
