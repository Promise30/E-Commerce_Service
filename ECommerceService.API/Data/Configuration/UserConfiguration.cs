using ECommerceService.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceService.API.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasOne(u => u.Cart).WithOne(c => c.User).HasForeignKey<Cart>(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
