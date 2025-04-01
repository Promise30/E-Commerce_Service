using System.ComponentModel;

namespace ECommerceService.API.Domain.Enums
{
    public enum RoleType
    {
        [Description("Admin")]
        Admin = 1,
        [Description("User")]
        User = 2

    }
}
