namespace ECommerceService.API.Helpers
{
    public class OrderFilterParameters : FilterParameters
    {
        public string? OrderStatus { get; set; } 
        public string? PaymentStatus { get; set; } 
        public string? PaymentMethod { get; set; } 
        public int? Duration { get; set; }
    }
}
