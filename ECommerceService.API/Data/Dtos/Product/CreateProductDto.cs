using System.ComponentModel.DataAnnotations;

namespace ECommerceService.API.Data.Dtos.Product
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "A product name is required")]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500, ErrorMessage = "Description cannot be more than 500 characters")]
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock Quantity cannot be a negative value")]
        public int StockQuantity { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price cannot be a negative value")]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
