using Microsoft.AspNetCore.Identity;

namespace ECommerceService.API.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneCountryCode { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }

    }
}
