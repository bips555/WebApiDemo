using Microsoft.EntityFrameworkCore;
using WebApiDemo.Models;

namespace WebApiDemo.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<Shirt> Shirts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Shirt>().HasData(
                new Shirt { ShirtId = 1, Size = 12, Brand = "A", Price = 12, Gender = "Men", Color = "Blue" },
            new Shirt { ShirtId = 2, Size = 11, Brand = "B", Price = 22, Gender = "Women", Color = "Red" },
            new Shirt { ShirtId = 3, Size = 14, Brand = "C", Price = 32, Gender = "Women", Color = "Green" },
            new Shirt { ShirtId = 4, Size = 13, Brand = "D", Price = 42, Gender = "Men", Color = "White" },
            new Shirt { ShirtId = 5, Size = 15, Brand = "E", Price = 52, Gender = "Women", Color = "Black" }


                );
        }
    }
}
