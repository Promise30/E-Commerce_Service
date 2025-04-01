using Microsoft.AspNetCore.SignalR;

namespace ECommerceService.API.Domain.Entities
{
    public class CartItem : Entity<int>
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
       // public decimal DiscountApplied { get; set; } = 0;
        //public decimal SubTotal => UnitPrice * Quantity;
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        //public string UserId { get; set; }
        //public virtual ApplicationUser User { get; set; }
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }

        public CartItem(int id) : base(id)
        {
        }
        public CartItem()
        {
        }
    }
}
