using ECommerceService.API.Domain.Entities;

namespace ECommerceService.API.Data.Dtos.CartItem
{
    public class CartDto : Entity<int>
    {
        public ICollection<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
        public Guid UserId { get; set; }
    }
}
