using System.ComponentModel.DataAnnotations;

namespace ECommerceService.API.Data.Dtos.Product
{
    public class UpdateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be greater than 0")]
        public int StockQuantity { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
