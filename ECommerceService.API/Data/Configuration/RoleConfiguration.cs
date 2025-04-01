using ECommerceService.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceService.API.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Description).HasMaxLength(200).IsRequired(false);
        }
    }
}
