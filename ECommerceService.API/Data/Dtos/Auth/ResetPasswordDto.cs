using System.ComponentModel.DataAnnotations;

namespace ECommerceService.API.Data.Dtos.Auth
{
    public class ResetPasswordDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
