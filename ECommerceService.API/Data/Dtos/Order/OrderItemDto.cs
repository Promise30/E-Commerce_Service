namespace ECommerceService.API.Data.Dtos.Order
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        //public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        //public decimal DiscountApplied { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
