using ECommerceService.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ECommerceService.API.Data.Configuration
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.UserId).IsRequired();
            builder.HasOne(cu => cu.User).WithOne(u => u.Cart).HasForeignKey<Cart>(c => c.UserId);
            builder.HasMany(c => c.CartItems).WithOne(ci => ci.Cart).OnDelete(DeleteBehavior.Cascade);
            builder.Navigation(c => c.CartItems).AutoInclude();
        }
    }
}
