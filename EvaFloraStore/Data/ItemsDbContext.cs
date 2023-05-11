using EvaFloraStore.Models;
using Microsoft.EntityFrameworkCore;

namespace EvaFloraStore.Data
{
    public class ItemsDbContext : DbContext 
    {
        public ItemsDbContext(DbContextOptions<ItemsDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
