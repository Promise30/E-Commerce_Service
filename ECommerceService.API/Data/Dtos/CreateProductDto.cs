using System.ComponentModel.DataAnnotations;

namespace ECommerceService.API.Data.Dtos
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
