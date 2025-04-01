using System.ComponentModel;

namespace ECommerceService.API.Domain.Enums
{
    public enum OrderStatus
    {
        [Description("Pending")]
        Pending = 1, 
        [Description("Processing")]
        Processing, 
        [Description("Shipped")]
        Shipped, 
        [Description("Delivered")]
        Delivered, 
        [Description("Cancelled")]
        Cancelled, 
        [Description("Failed")]
        Failed, 
        [Description("Refunded")]
        Refunded, 
    }
}
