using Microsoft.EntityFrameworkCore;
using Shop.DAL.Models;

namespace Shop.DAL.Helpers
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Books"
                },
                new Category
                {
                    Id = 2,
                    Name = "Sports Appliances"
                },
                new Category
                {
                    Id = 3,
                    Name = "Musical Instruments"
                }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Harry Potter",
                    Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsumLoremipsum",
                    CategoryId = 1,
                    Price = 20,
                    Unit = "Piece",
                    Quantity = 20
                },
                new Product
                {
                    Id = 2,
                    Name = "Harry Potter 2",
                    Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsumLoremipsum",
                    CategoryId = 1,
                    Price = 20,
                    Unit = "Piece",
                    Quantity = 20
                }
                );
        }
    }
}
