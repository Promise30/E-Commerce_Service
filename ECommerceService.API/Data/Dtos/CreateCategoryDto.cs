using System.ComponentModel.DataAnnotations;

namespace ECommerceService.API.Data.Dtos
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
