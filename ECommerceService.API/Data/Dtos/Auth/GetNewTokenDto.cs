using System.ComponentModel.DataAnnotations;

namespace ECommerceService.API.Data.Dtos.Auth
{
    public class GetNewTokenDto
    {
        [Required(ErrorMessage = "Access token is required")]
        public string AccessToken { get; set; }
        [Required(ErrorMessage = "Refresh token is required")]
        public string RefreshToken { get; set; }
    }
}
