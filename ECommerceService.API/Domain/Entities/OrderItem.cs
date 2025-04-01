namespace ECommerceService.API.Domain.Entities
{
    public class OrderItem : Entity<int>
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public OrderItem() { }
        public OrderItem(int id) { }
    }
}
