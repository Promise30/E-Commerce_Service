using System.ComponentModel;

namespace ECommerceService.API.Domain.Enums
{
    public enum PaymentStatus
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Paid")]
        Paid,
        [Description("Failed")]
        Failed,
        [Description("Refunded")]
        Refunded
    }
}
