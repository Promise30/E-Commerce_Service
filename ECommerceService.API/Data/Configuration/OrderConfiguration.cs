using ECommerceService.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ECommerceService.API.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.TotalAmount).IsRequired().HasPrecision(18, 2);
            builder.Property(o => o.DiscountApplied).IsRequired().HasPrecision(18, 2);
            builder.Property(o => o.TaxAmount).IsRequired().HasPrecision(18, 2);
            builder.Property(o => o.UserId).IsRequired();
            builder.HasMany(o => o.OrderItems).WithOne(u => u.Order).HasForeignKey(o => o.OrderId);
            builder.HasOne(o=> o.User).WithMany(u=> u.Orders).HasForeignKey(o => o.UserId);
            builder.Navigation(o => o.OrderItems).AutoInclude(true);
            builder.Navigation(o => o.User).AutoInclude(true);
        }
    }
}
