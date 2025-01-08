namespace ECommerceService.API.Data.Dtos
{
    public class ApplicationRoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
    }
}
