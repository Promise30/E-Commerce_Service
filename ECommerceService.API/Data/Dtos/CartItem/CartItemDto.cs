using ECommerceService.API.Domain.Entities;

namespace ECommerceService.API.Data.Dtos.CartItem
{
    public class CartItemDto : Entity<int>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int CartId { get; set; }
    }
}
