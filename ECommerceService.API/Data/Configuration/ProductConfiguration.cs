using ECommerceService.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Reflection.Emit;

namespace ECommerceService.API.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            var random = new Random();
            var startDate = new DateTime(2024, 3, 28); // One year ago
            var endDate = new DateTime(2025, 3, 28);   // Current date

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.HasIndex(p => p.Name).IsUnique();
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.Property(p => p.StockQuantity).IsRequired();
            builder.Property(p => p.Price).HasPrecision(18, 2);
            builder.HasOne(p => p.Category).WithMany(p => p.Products).HasForeignKey(p => p.CategoryId);
            // builder.Property(p=> p.CreatedDate).HasDefaultValueSql("GETDATE()");
            // builder.Property(p => p.LastModifiedDate).HasDefaultValueSql("GETDATE()");

            builder.HasData(
            new Product { Id = 1, Name = "Laptop", Description = "A rich and well-equipped high performance machine", StockQuantity = 100, Price = 1000, CategoryId = 1, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
            new Product { Id = 2, Name = "Mobile", Description = "A pocket friendly and highly efficient telecommunication device", StockQuantity = 100, Price = 500, CategoryId = 1, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 3, Name = "T-Shirt", Description = "A warm and cold weather outfit for many genders", StockQuantity = 100, Price = 20, CategoryId = 2, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 4, Name = "Jeans", Description = "A lower body covering suitable for various activities", StockQuantity = 100, Price = 50, CategoryId = 2, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 5, Name = "Harry Potter: Volume 1", Description = "The Philosopher's stone which looks at the life of Harry", StockQuantity = 100, Price = 10, CategoryId = 3, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 6, Name = "Mission Impossible", Description = "A story starring a man with no fears", StockQuantity = 100, Price = 15, CategoryId = 3, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 7, Name = "The Alchemist", Description = "A story of a determined and resilient warrior", StockQuantity = 50, Price = 65, CategoryId = 3, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 8, Name = "Smartphone", Description = "A sleek and powerful mobile device", StockQuantity = 150, Price = 599.99m, CategoryId = 3, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 9, Name = "Headphones", Description = "Noise-canceling over-ear audio gear", StockQuantity = 75, Price = 129.50m, CategoryId = 5, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 10, Name = "Desk Chair", Description = "Ergonomic chair for long work hours", StockQuantity = 30, Price = 199.00m, CategoryId = 10, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 11, Name = "Monitor", Description = "4K ultra-wide display for productivity", StockQuantity = 50, Price = 349.95m, CategoryId = 1, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 12, Name = "Keyboard", Description = "Mechanical keyboard with RGB lighting", StockQuantity = 200, Price = 89.99m, CategoryId = 7, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 13, Name = "Mouse", Description = "Precision gaming mouse with adjustable DPI", StockQuantity = 180, Price = 49.99m, CategoryId = 7, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 14, Name = "Tablet", Description = "Lightweight tablet for media and work", StockQuantity = 90, Price = 299.00m, CategoryId = 3, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 15, Name = "Printer", Description = "All-in-one printer with wireless connectivity", StockQuantity = 25, Price = 159.75m, CategoryId = 9, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 16, Name = "Backpack", Description = "Durable backpack for travel and tech", StockQuantity = 60, Price = 79.50m, CategoryId = 8, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 17, Name = "Smartwatch", Description = "Fitness tracker with heart rate monitor", StockQuantity = 120, Price = 199.99m, CategoryId = 4, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 18, Name = "Camera", Description = "High-resolution DSLR for photography", StockQuantity = 15, Price = 799.00m, CategoryId = 6, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 19, Name = "Speakers", Description = "Bluetooth speakers with deep bass", StockQuantity = 80, Price = 99.95m, CategoryId = 5, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 20, Name = "Router", Description = "High-speed Wi-Fi router for home", StockQuantity = 40, Price = 129.00m, CategoryId = 8, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 21, Name = "External Drive", Description = "1TB portable SSD for storage", StockQuantity = 100, Price = 149.99m, CategoryId = 10, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 22, Name = "Coffee Maker", Description = "Automatic coffee machine with timer", StockQuantity = 35, Price = 89.00m, CategoryId = 7, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 23, Name = "Gaming Console", Description = "Next-gen console for immersive gaming", StockQuantity = 20, Price = 499.99m, CategoryId = 2, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 24, Name = "Projector", Description = "HD projector for home theater", StockQuantity = 10, Price = 399.50m, CategoryId = 2, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 25, Name = "Fitness Band", Description = "Lightweight band for step tracking", StockQuantity = 140, Price = 39.99m, CategoryId = 4, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 26, Name = "Desk Lamp", Description = "Adjustable LED lamp for workspace", StockQuantity = 70, Price = 29.95m, CategoryId = 4, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) },
                new Product { Id = 27, Name = "Microwave", Description = "Compact microwave for quick meals", StockQuantity = 45, Price = 109.00m, CategoryId = 5, CreatedDate = GenerateRandomDateTime(random, startDate, endDate), LastModifiedDate = GenerateRandomDateTime(random, startDate, endDate) }
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
