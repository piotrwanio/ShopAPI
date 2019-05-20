using Microsoft.EntityFrameworkCore;
using Shop.DAL.Models;

namespace Shop.DAL.Helpers
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
