using ECommerceService.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ECommerceService.API.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
       
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            var random = new Random();
            var startDate = new DateTime(2024, 3, 28); // One year ago
            var endDate = new DateTime(2025, 3, 28);   // Current date

            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Name).IsUnique();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.HasMany(c=> c.Products).WithOne(p=> p.Category).HasForeignKey(p=>p.Id);
            //builder.Property(c=> c.CreatedDate).HasDefaultValueSql("GETDATE()");
            //builder.Property(c => c.LastModifiedDate).HasDefaultValueSql("GETDATE()");

            builder.HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Electronic Items", CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Category { Id = 2, Name = "Clothes", Description = "Clothing Items", CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Category { Id = 3, Name = "Books", Description = "Books Items", CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Category { Id = 4, Name = "Devices", Description = "Devices and gadgets for everyday use", CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Category { Id = 5, Name = "Furniture", Description = "Home and office furnishing items", CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Category { Id = 6, Name = "Outfits", Description = "Apparel for all occasions", CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Category { Id = 7, Name = "Journals", Description = "Literature and educational materials", CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Category { Id = 8, Name = "Appliances", Description = "Household appliances for convenience", CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Category { Id = 9, Name = "Accessories", Description = "Add-ons for tech and lifestyle", CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Category { Id = 10, Name = "Sports", Description = "Gear and equipment for sports enthusiasts", CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) }
            );
        }

    
    private static DateTimeOffset GenerateRandomDateTime(Random random, DateTime startDate, DateTime endDate)
        {
            int range = (endDate - startDate).Days;
            int randomDays = random.Next(0, range + 1);
            int randomHours = random.Next(0, 24);
            int randomMinutes = random.Next(0, 60);
            int randomSeconds = random.Next(0, 60);
            int randomMilliseconds = random.Next(0, 1000);

            return startDate
                .AddDays(randomDays)
                .AddHours(randomHours)
                .AddMinutes(randomMinutes)
                .AddSeconds(randomSeconds)
                .AddMilliseconds(randomMilliseconds);
        }
    }
}
