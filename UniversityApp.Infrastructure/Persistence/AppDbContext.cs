using Microsoft.EntityFrameworkCore;
using UniversityApp.Core.Entities;

namespace UniversityApp.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }

        // Add more DbSets later for Category, Order, etc.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API configs here if needed
        }
    }
}
