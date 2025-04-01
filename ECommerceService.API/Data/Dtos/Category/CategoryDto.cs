using ECommerceService.API.Data.Dtos.Product;

namespace ECommerceService.API.Data.Dtos.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public ICollection<ProductDto> Products { get; set; }
    }
}
