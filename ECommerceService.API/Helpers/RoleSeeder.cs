using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace ECommerceService.API.Helpers
{
    public static class RoleSeeder 
    {
        public static async Task SeedRole(this IServiceProvider serviceProvider)
        {
            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                var roles = Enum.GetValues<RoleType>().Select(r => (r).GetEnumDescription());

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        var newRole = new ApplicationRole
                        {
                            Name = role,
                            NormalizedName = role.ToUpper(),
                            DateCreated = DateTime.Now,
                            LastModifiedDate = DateTime.Now
                        };
                        await roleManager.CreateAsync(newRole);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
