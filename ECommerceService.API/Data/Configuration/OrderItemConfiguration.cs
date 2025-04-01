using ECommerceService.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceService.API.Data.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.UnitPrice).IsRequired().HasPrecision(18, 2);
            builder.Property(oi => oi.Quantity).IsRequired();
            builder.Property(oi => oi.SubTotal).IsRequired().HasPrecision(18, 2);
            builder.Property(oi => oi.ProductId).IsRequired();
            builder.HasOne(oi=> oi.Product).WithMany(p=> p.OrderItems).HasForeignKey(oi => oi.ProductId);
            builder.HasOne(oi => oi.Order).WithMany(o => o.OrderItems).HasForeignKey(oi => oi.OrderId);
        }
    }
}
