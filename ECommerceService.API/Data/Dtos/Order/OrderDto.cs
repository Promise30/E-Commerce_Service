namespace ECommerceService.API.Data.Dtos.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountApplied { get; set; }
        public string UserId { get; set; }
        public string? CustomerFirstName { get; set; }
        public string? CustomerLastName { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

    }
}
