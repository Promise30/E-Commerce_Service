namespace ECommerceService.API.Domain.Entities
{
    public class Cart : Entity<int>
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public Cart(int id) : base(id) 
        { 
        }
        public Cart()
        {
        }
    }
}
