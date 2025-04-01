namespace ECommerceService.API.Data.Dtos.Auth
{
    public class ApplicationRoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
    }
}
