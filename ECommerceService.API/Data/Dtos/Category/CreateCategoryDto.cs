using System.ComponentModel.DataAnnotations;

namespace ECommerceService.API.Data.Dtos.Category
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        [MaxLength(500, ErrorMessage = "Description cannot be more than 500 characters")]
        public string? Description { get; set; }
    }
}
