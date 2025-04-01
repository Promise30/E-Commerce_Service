namespace ECommerceService.API.Data.Dtos.Auth
{
    public class ApplicationUserDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneCountryCode { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool TwoFactorEnabled { get; set; }
        public List<string?> Roles { get; set; } = new List<string?>();
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }

    }
}
