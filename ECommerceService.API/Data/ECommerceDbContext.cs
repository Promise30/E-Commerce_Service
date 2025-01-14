
using ECommerceService.API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceService.API.Data
{
    public class ECommerceDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfigurationsFromAssembly(typeof(ECommerceDbContext).Assembly);
            modelBuilder.Entity<Product>().Property(p=> p.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(p=> p.Description).HasMaxLength(500);
            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Category>().Property(c=> c.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Category>().Property(c=> c.Description).HasMaxLength(500);

            modelBuilder.Entity<Product>().HasOne(p=> p.Category).WithMany(c=> c.Products).HasForeignKey(p=> p.CategoryId);

            base.OnModelCreating(modelBuilder); 
        }
    }
}
