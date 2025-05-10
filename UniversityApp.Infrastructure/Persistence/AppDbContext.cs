using Microsoft.EntityFrameworkCore;
using UniversityApp.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace UniversityApp.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }

        // Add more DbSets later for Category, Order, etc.

        public DbSet<Student> Students { get; set; }

        public DbSet<Professor> Professors { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Department> Departments { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API configs here if needed
        }
    }
}
