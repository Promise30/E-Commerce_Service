using ECommerceService.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ECommerceService.API.Data.Configuration
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(ci => ci.Id);
            builder.Property(ci => ci.UnitPrice).HasPrecision(18, 2);
            builder.Property(ci => ci.Quantity).IsRequired();
            builder.Property(ci=> ci.ProductId).IsRequired();
            builder.HasOne(ci => ci.Product).WithMany(p => p.CartItems).HasForeignKey(ci => ci.ProductId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(ci => ci.Cart).WithMany(u => u.CartItems).HasForeignKey(ci => ci.CartId);
            builder.Navigation(ci => ci.Product).AutoInclude(true);
        }
    }
}
