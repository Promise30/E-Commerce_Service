namespace ECommerceService.API.Data.Dtos.Order
{
    public class PaymentInitializationResponse
    {
        public string AuthorizationUrl { get; set; } = null!;
        public string PaymentReference { get; set; } = null!;
    }
}
